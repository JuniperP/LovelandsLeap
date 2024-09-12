using System;
using Godot;

public class WalkState : MovementState
{
	private double _coyoteTime = 0d;
	private double _jumpBufferTime = Mathf.Inf;
	private double _fallingTime = 0d;
	private bool _isFastFalling = false;

	// TODO: Walk sound should be managed by SoundManager
	private double _walkSoundLeft = 0d;

	// Constructor that calls parent constructor (weird notation)
	public WalkState(Player ctx) : base(ctx) { }

	public override void HandleMovement(double delta)
	{
		// Temporary variables that we use to perform logic and math
		Vector2 targetVelocity = Ctx.Velocity;
		AnimState newState = Ctx.AnimManager.State;

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

		// If in the air, change acceleration by some factor
		if (!floored)
			accelFactor *= Ctx.AccelAirFactor;

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

		// Play walking sound if moving and there's no time left on the walk SFX
		if (floored && Mathf.Abs(velocity) > (0.9f * Ctx.Speed) && _walkSoundLeft <= 0d)
		{
			Random rand = new();

			// Play random walk sound
			SFX sfx = (SFX)rand.Next((int)SFX.Walk1, (int)SFX.Walk4 + 1);
			float volume = (float)rand.NextDouble() * 3f - 3f;
			float pitch = (float)rand.NextDouble() * 0.3f + 0.85f;
			SoundManager.PlaySound(sfx, Ctx, volume, pitch);

			// Set random remaining walk time
			_walkSoundLeft = rand.NextDouble() * 0.3 + 0.3;
		}
		else // Reduce walk sound time
			_walkSoundLeft -= delta;

		return velocity;
	}

	private float VerticalVelocity(double delta, float velocity, bool floored)
	{
		// Kill vertical velocity when hitting ceiling
		if (Ctx.IsOnCeiling() && velocity < -0.1f)
			velocity = 0f;

		// Handle gravity
		float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");

		// Handle jumping
		if (Input.IsActionJustPressed("move_up"))
		{
			// Jump if player is on the ground or has coyote frames
			if (floored || (_coyoteTime < Ctx.MaxCoyoteTime && velocity > 0.1f))
			{
				velocity = -Ctx.JumpImpulse;
				SoundManager.PlaySound(SFX.Jump, Ctx);
			}
			else // Buffer a jump input
				_jumpBufferTime = 0d;
		}

		if (floored)
		{
			// If player has recently buffered a jump (and isn't already jumping), perform one
			if (_jumpBufferTime < Ctx.JumpBufferTime && velocity != -Ctx.JumpImpulse)
			{
				SoundManager.PlaySound(SFX.Jump, Ctx);
				velocity = -Ctx.JumpImpulse;

				// Perform jump cut immediately if the jump button is no longer held
				if (!Input.IsActionPressed("move_up"))
					velocity *= Ctx.JumpCutFactor;
			}

			// Accounting for free fall and subsequent landing sfx
			if (_fallingTime > 0.1)
				SoundManager.PlaySound(SFX.Land, Ctx);

			// Reset variables
			_isFastFalling = false;
			_coyoteTime = 0d;
			_jumpBufferTime = Mathf.Inf;
			_fallingTime = 0d;
		}
		else // Midair
		{
			// Increment jump buffer if active
			if (_jumpBufferTime != Mathf.Inf)
				_jumpBufferTime += delta;

			// Increment coyote and fall time
			_coyoteTime += delta;
			_fallingTime += delta;

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

	public override void EnableGrapple(Node2D target)
	{

		// Create spring and update line
		Ctx.TongueSpring = Ctx.TongueSpringScene.Instantiate<TongueSpring>();
		Ctx.TongueSpring.GlobalPosition = Ctx.TongueProj.GlobalPosition;
		Ctx.TongueSpring.AttachedTo = target;
		Ctx.TongueSpring.Remote = new RemoteTransform2D();
		Ctx.TongueSpringRemote = Ctx.TongueSpring.Remote;
		Ctx.TongueLine.Target = Ctx.TongueSpring;

		// Remove tongue projectile
		Ctx.TongueProj.QueueFree();

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
