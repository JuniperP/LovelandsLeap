using Godot;

public class WalkState : MovementState
{
	private bool _isFastFalling = false;

	// Track if the player was in the air last frame
	private bool _wasInAir = false;

	// Constructor that calls parent constructor (weird notation)
	public WalkState(Player ctx) : base(ctx) { }

	public override void HandleMovement(double delta)
	{
		// Temporary variables that we use to perform logic and math
		Vector2 targetVelocity = Ctx.Velocity;
		AnimState newState = Ctx.AnimManager.State;

		// Kill vertical velocity when hitting ceiling
		if (Ctx.IsOnCeiling())
			targetVelocity.Y = 0;

		bool isFloored = Ctx.IsOnFloor();
		// Horizontal input direction in range [-1f, 1f]
		float direction = Input.GetAxis("move_left", "move_right");

		// Calculate new velocity using helper methods
		targetVelocity.X = HorizontalVelocity(delta, targetVelocity.X, direction, isFloored);
		targetVelocity.Y = VerticalVelocity(delta, targetVelocity.Y, isFloored);

		// Handle sprite direction
		if (direction > 0.01f)
			Ctx.AnimManager.IsLeftFacing = false;
		else if (direction < -0.01f)
			Ctx.AnimManager.IsLeftFacing = true;

		// Update sprite state
		if (newState != AnimState.Tongue) // If using tongue, do not change animation
		{
			if (isFloored) // If floored, walk if moving else idle
				newState = Mathf.Abs(targetVelocity.X) > 0.01f
				? AnimState.Walking : AnimState.Idle;
			else // If in the air, use jump animation
				newState = AnimState.Jumping;
		}
		Ctx.AnimManager.State = newState;

		// Update velocity and move
		Ctx.Velocity = targetVelocity;
		Ctx.MoveAndSlide();
	}

	private float HorizontalVelocity(double delta, float velocity, float direction, bool floored)
	{
		// Used to determine what the final acceleration will be
		float accelFactor = 1f;

		// Accounting for free fall and subsequent landing sfx
		if (floored && _wasInAir)
		{
			SoundManager.PlaySound(SFX.Land, Ctx);
			_wasInAir = false;
		}
		// If in the air, change acceleration by some factor
		else if (!floored)
		{
			accelFactor *= Ctx.AccelAirFactor;
			_wasInAir = true;
		}

		// If trying to move in opposite direction
		if (Mathf.Sign(velocity) != Mathf.Sign(direction))
			accelFactor *= Ctx.AccelOppositionFactor;
		// If same direction but going faster than walk speed
		else if (Mathf.Abs(velocity) > Ctx.Speed)
		{
			// Exponentially increase acceleration based on player speed
			float speedRatio = Mathf.Abs(velocity) / Ctx.Speed;
			float expSlowing = Mathf.Pow(Ctx.AccelSpeedingBase, speedRatio - 1);
			accelFactor *= expSlowing * Ctx.AccelSpeedingFactor;
		}

		// Smooth velocity towards input direction
		velocity = Mathf.MoveToward(
			Ctx.Velocity.X,  // Starting velocity
			direction * Ctx.Speed,  // Target velocity
			Ctx.Acceleration * accelFactor * (float)delta  // How much to adjust by
		);

		// Adjusting for the grounds velocity
		velocity = velocity * Ctx.GetPlatformVelocity().X;

		return velocity;
	}

	private float VerticalVelocity(double delta, float velocity, bool floored)
	{
		// Handle gravity
		float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");

		if (floored)
		{
			_isFastFalling = false;

			// Handle jump
			if (Input.IsActionJustPressed("move_up"))
			{
				velocity = -Ctx.JumpImpulse;
				SoundManager.PlaySound(SFX.Jump, Ctx);
			}
		}
		else  // Midair
		{
			if (Input.IsActionJustPressed("move_down"))
				_isFastFalling = true;

			float velIncrease = gravity * Ctx.GravityFactor * (float)delta;
			if (velocity > 0.1f) // If falling, increase gravity
				velIncrease *= Ctx.FallGravityFactor;
			if (_isFastFalling) // If fast falling, increase gravity
				velIncrease *= Ctx.FastFallFactor;
			velocity += velIncrease;
		}

		// If you release jump while going up, reduce velocity
		if (Input.IsActionJustReleased("move_up") && velocity < -0.1f)
			velocity *= Ctx.JumpCutFactor;

		// Cap vertical speed
		float maxFall = Ctx.MaxFallSpeed;
		if (_isFastFalling)
			maxFall *= Ctx.FastFallMaxFactor;
		velocity = Mathf.Min(velocity, maxFall);

		// Adjusting for the grounds velocity
		velocity = velocity * Ctx.GetPlatformVelocity().Y;

		return velocity;
	}

	public override void HandleActions()
	{
		// If trying to create a new tongue shot with no pre-existing
		if (!Ctx.TongueProj.IsValid() && Input.IsActionJustPressed("primary_action"))
		{
			// Set animation to tongue
			Ctx.AnimManager.State = AnimState.Tongue;

			// Get mouse direction
			Vector2 mousePos = Ctx.GetGlobalMousePosition();
			Vector2 mouseDirection = Ctx.TongueGlobalPos.DirectionTo(mousePos);

			// Set mouse direction to minimum angle if outside of accepted angle
			float minSin = -Mathf.Sin(Mathf.DegToRad(Ctx.TongueAngle));
			if (mouseDirection.Y > minSin)
			{
				float minCos = Mathf.Cos(Mathf.DegToRad(Ctx.TongueAngle));
				minCos *= Mathf.Sign(mouseDirection.X);
				mouseDirection = new Vector2(minCos, minSin);
			}

			// Create and configure tongue projectile
			Ctx.TongueProj = Ctx.TongueProjScene.Instantiate<TongueProjectile>();
			Ctx.TongueProj.Setup(Ctx.EnableGrapple, mouseDirection);
			Ctx.AddChild(Ctx.TongueProj);

			// Create and configure tongue line
			Ctx.TongueLine = Ctx.TongueLineScene.Instantiate<TongueLine>();
			Ctx.TongueLine.Target = Ctx.TongueProj;
			Ctx.TongueProj.TongueLine = Ctx.TongueLine;
			Ctx.AddChild(Ctx.TongueLine);
			Ctx.TongueLine.GlobalPosition = Ctx.TongueGlobalPos;

			// Activate tongue shoot sound effect
			SoundManager.PlaySound(SFX.TongueShoot, Ctx);
		}
		// Retract tongue if it is being used
		else if (Ctx.TongueProj.IsValid() && Input.IsActionJustPressed("secondary_action"))
		{
			Ctx.TongueProj.RetractTongue();
			Ctx.AnimManager.State = AnimState.Idle;
		}
	}

	public override void EnableGrapple()
	{
		// Remove tongue projectile
		Ctx.TongueProj.QueueFree();

		// Create spring and update line
		Ctx.TongueSpring = Ctx.TongueSpringScene.Instantiate<TongueSpring>();
		Ctx.TongueSpring.GlobalPosition = Ctx.TongueProj.GlobalPosition;
		Ctx.TongueLine.Target = Ctx.TongueSpring;

		// Create tongue weight (pendulee)
		Ctx.TongueWeight = Ctx.TongueWeightScene.Instantiate<RigidBody2D>();
		Ctx.TongueSpring.Target = Ctx.TongueWeight;
		Ctx.TongueWeight.GlobalPosition = Ctx.GlobalPosition;

		// Add spring and weight to scene
		// The code creates exceptions without performing a deferred call
		Ctx.CallDeferred(Node.MethodName.AddSibling, Ctx.TongueWeight);
		Ctx.CallDeferred(Node.MethodName.AddSibling, Ctx.TongueSpring);

		// Change state to grapple
		Ctx.StateEnum = Player.State.Grapple;

		// Activate tongue hit effect
		SoundManager.PlaySound(SFX.TongueHit, Ctx);
	}

	public override void DisableGrapple() { }
}
