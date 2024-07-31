using Godot;

public class WalkState : MovementState
{
	// Boolean to track if the player is fast falling
	private bool _fastFall = false;

	// Boolean to track if the player was in the air last frame
	private bool _wasInAir = false;

	public WalkState(Player ctx) : base(ctx) { }

	public override void HandleMovement(double delta)
	{
		Vector2 targetVelocity = Ctx.Velocity;
		AnimState newState = Ctx.AnimManager.State;

		// Kill vertical velocity when hitting ceiling
		if (Ctx.IsOnCeiling())
			targetVelocity.Y = 0;

		bool floored = Ctx.IsOnFloor();
		float direction = Input.GetAxis("move_left", "move_right");

		targetVelocity.X = HorizontalVelocity(delta, targetVelocity.X, direction, floored);
		targetVelocity.Y = VerticalVelocity(delta, targetVelocity.Y, floored);

		// Handle sprite direction
		if (direction > 0.01f)
			Ctx.AnimManager.IsLeftFacing = false;
		else if (direction < -0.01f)
			Ctx.AnimManager.IsLeftFacing = true;

		if (newState != AnimState.Tongue) // If using tongue, do not change animation
		{
			if (floored) // If floored, walk if moving else idle
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
		float accelFactor = 1f;

		// Accounting for free fall and subsequent landing sfx
		if (floored && _wasInAir)
		{
			SoundManager.PlaySound(SFX.Land, Ctx);
			_wasInAir = false;
		}
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
			float speedRatio = Mathf.Abs(velocity) / Ctx.Speed;
			float expSlowing = Mathf.Pow(Ctx.AccelSpeedingBase, speedRatio - 1);
			accelFactor *= expSlowing * Ctx.AccelSpeedingFactor;
		}

		// Smooth velocity towards input direction
		velocity = Mathf.MoveToward(
			Ctx.Velocity.X,
			direction * Ctx.Speed,
			Ctx.Acceleration * accelFactor * (float)delta
		);

		return velocity;
	}

	private float VerticalVelocity(double delta, float velocity, bool floored)
	{
		// Handle gravity
		float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
		if (floored)
		{
			_fastFall = false;

			// Handle jump
			if (Input.IsActionJustPressed("move_up"))
			{
				velocity = -Ctx.JumpImpulse;
				SoundManager.PlaySound(SFX.Jump, Ctx);
			}
		}
		else
		{
			if (Input.IsActionJustPressed("move_down"))
				_fastFall = true;

			float velIncrease = gravity * Ctx.GravityFactor * (float)delta;
			if (velocity > 0.1f) // If falling, increase gravity
				velIncrease *= Ctx.FallGravityFactor;
			if (_fastFall) // If fast falling, increase gravity
				velIncrease *= Ctx.FastFallFactor;
			velocity += velIncrease;
		}

		// Handle jump cut
		if (Input.IsActionJustReleased("move_up") && velocity < -0.1f)
			velocity *= Ctx.JumpCutFactor;

		// Cap vertical speed
		float maxFall = Ctx.MaxFallSpeed;
		if (_fastFall)
			maxFall *= Ctx.FastFallMaxFactor;
		velocity = Mathf.Min(velocity, maxFall);

		return velocity;
	}

	public override void HandleAction()
	{
		if (Input.IsActionJustPressed("primary_action"))
		{
			if (Ctx.TongueProj.IsValid()) // Retract if using tongue
			{
				Ctx.TongueProj.RetractTongue();
				Ctx.AnimManager.State = AnimState.Idle;
				return;
			}

			// Set animation to tongue
			Ctx.AnimManager.State = AnimState.Tongue;

			Vector2 mousePos = Ctx.GetGlobalMousePosition();
			Vector2 direction = (mousePos - Ctx.TongueGlobalPos).Normalized();

			// Set direction to minimum angle if outside of accepted angle
			float minSin = -Mathf.Sin(Mathf.DegToRad(Ctx.TongueAngle));
			if (direction.Y > minSin)
			{
				float minCos = Mathf.Cos(Mathf.DegToRad(Ctx.TongueAngle));
				minCos *= Mathf.Sign(direction.X);
				direction = new Vector2(minCos, minSin);
			}

			// Create tongue projectile
			Ctx.TongueProj = Ctx.TongueProjScene.Instantiate<TongueProjectile>();
			Ctx.TongueProj.Setup(Ctx.EnableGrapple, direction);
			Ctx.AddChild(Ctx.TongueProj);

			// Create tongue line
			Ctx.TongueLine = Ctx.TongueLineScene.Instantiate<TongueLine>();
			Ctx.TongueLine.Target = Ctx.TongueProj;
			Ctx.TongueProj.TongueLine = Ctx.TongueLine;
			Ctx.AddChild(Ctx.TongueLine);
			Ctx.TongueLine.GlobalPosition = Ctx.TongueGlobalPos;

			// Activate tongue shoot sound effect
			SoundManager.PlaySound(SFX.TongueShoot, Ctx);
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

		// Create tongue weight
		Ctx.TongueWeight = Ctx.TongueWeightScene.Instantiate<RigidBody2D>();
		Ctx.TongueSpring.Target = Ctx.TongueWeight;
		Ctx.TongueWeight.GlobalPosition = Ctx.GlobalPosition;

		// Add spring and weight to scene
		Ctx.CallDeferred(Node.MethodName.AddSibling, Ctx.TongueWeight);
		Ctx.CallDeferred(Node.MethodName.AddSibling, Ctx.TongueSpring);

		// Change state to grapple
		Ctx.StateEnum = Player.State.Grapple;

		//Activate tongue hit effect
		SoundManager.PlaySound(SFX.TongueHit, Ctx);
	}

	public override void DisableGrapple() { }
}
