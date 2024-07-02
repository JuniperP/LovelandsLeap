using System;
using Godot;

public class WalkState : MovementState
{
	private bool _fastFall = false;

	public WalkState(Player ctx) : base(ctx) { }

	public override void HandleMovement(double delta)
	{
		Vector2 targetVelocity = _ctx.Velocity;
		AnimState newState = _ctx.AnimManager.State;

		// Kill vertical velocity when hitting ceiling
		if (_ctx.IsOnCeiling())
			targetVelocity.Y = 0;

		bool floored = _ctx.IsOnFloor();
		float direction = Input.GetAxis("move_left", "move_right");

		targetVelocity.X = HorizontalVelocity(delta, targetVelocity.X, direction, floored);
		targetVelocity.Y = VerticalVelocity(delta, targetVelocity.Y, floored);

		// Handle sprite direction
		if (direction > 0.01f)
			_ctx.AnimManager.IsLeftFacing = false;
		else if (direction < -0.01f)
			_ctx.AnimManager.IsLeftFacing = true;

		if (newState != AnimState.Tongue) // If using tongue, do not change animation
		{
			if (floored) // If floored, walk if moving else idle
				newState = Mathf.Abs(targetVelocity.X) > 0.01f
				? AnimState.Walking : AnimState.Idle;
			else // If in the air, use jump animation
				newState = AnimState.Jumping;
		}
		_ctx.AnimManager.State = newState;

		// Update velocity and move
		_ctx.Velocity = targetVelocity;
		_ctx.MoveAndSlide();
	}

	private float HorizontalVelocity(double delta, float velocity, float direction, bool floored)
	{
		float accelFactor = 1f;

		if (!floored)
			accelFactor *= _ctx.AccelAirFactor;

		// If trying to move in opposite direction
		if (Mathf.Sign(velocity) != Mathf.Sign(direction))
			accelFactor *= _ctx.AccelOppositionFactor;
		// If same direction but going faster than walk speed
		else if (Mathf.Abs(velocity) > _ctx.Speed)
			accelFactor *= _ctx.AccelSpeedingFactor;

		// Smooth velocity towards input direction
		velocity = Mathf.MoveToward(
			_ctx.Velocity.X,
			direction * _ctx.Speed,
			_ctx.Acceleration * accelFactor * (float)delta
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
				velocity = -_ctx.JumpImpulse;
				SoundManager.PlaySound(SFX.Jump, _ctx);
			}
				
		}
		else
		{
			if (Input.IsActionJustPressed("move_down"))
				_fastFall = true;

			float velIncrease = gravity * _ctx.GravityFactor * (float)delta;
			if (velocity > 0.1f) // If falling, increase gravity
				velIncrease *= _ctx.FallGravityFactor;
			if (_fastFall) // If fast falling, increase gravity
				velIncrease *= _ctx.FastFallFactor;
			velocity += velIncrease;
		}

		// Handle jump cut
		if (Input.IsActionJustReleased("move_up") && velocity < -0.1f)
			velocity *= _ctx.JumpCutFactor;

		// Cap vertical speed
		float maxFall = _ctx.MaxFallSpeed;
		if (_fastFall)
			maxFall *= _ctx.FastFallMaxFactor;
		velocity = Mathf.Min(velocity, maxFall);

		return velocity;
	}

	public override void HandleAction()
	{
		if (Input.IsActionJustPressed("primary_click"))
		{
			if (_ctx.TongueProj.IsValid()) // Retract if using tongue
			{
				_ctx.TongueProj.RetractTongue(
					_ctx.GlobalPosition - _ctx.TongueProj.GlobalPosition
				);
				_ctx.AnimManager.State = AnimState.Idle;
				return;
			}

			// Set animation to tongue
			_ctx.AnimManager.State = AnimState.Tongue;

			Vector2 mousePos = _ctx.GetGlobalMousePosition();
			Vector2 direction = (mousePos - _ctx.TongueGlobalPos).Normalized();

			// Set direction to minimum angle if outside of accepted angle
			float minSin = -Mathf.Sin(Mathf.DegToRad(_ctx.TongueAngle));
			if (direction.Y > minSin)
			{
				float minCos = Mathf.Cos(Mathf.DegToRad(_ctx.TongueAngle));
				minCos *= Mathf.Sign(direction.X);
				direction = new Vector2(minCos, minSin);
			}

			// Create tongue projectile
			_ctx.TongueProj = _ctx.TongueProjScene.Instantiate<TongueProjectile>();
			_ctx.TongueProj.Setup(_ctx.EnableGrapple, direction);
			_ctx.AddChild(_ctx.TongueProj);

			// Create tongue line
			_ctx.TongueLine = _ctx.TongueLineScene.Instantiate<TongueLine>();
			_ctx.TongueLine.Target = _ctx.TongueProj;
			_ctx.TongueProj.TongueLine = _ctx.TongueLine;
			_ctx.AddChild(_ctx.TongueLine);
			_ctx.TongueLine.GlobalPosition = _ctx.TongueGlobalPos;
		}
	}

	public override void EnableGrapple()
	{
		// Remove tongue projectile
		_ctx.TongueProj.QueueFree();

		// Create spring and update line
		_ctx.TongueSpring = _ctx.TongueSpringScene.Instantiate<TongueSpring>();
		_ctx.TongueSpring.GlobalPosition = _ctx.TongueProj.GlobalPosition;
		_ctx.TongueLine.Target = _ctx.TongueSpring;

		// Create tongue weight
		_ctx.TongueWeight = _ctx.TongueWeightScene.Instantiate<RigidBody2D>();
		_ctx.TongueSpring.Target = _ctx.TongueWeight;
		_ctx.TongueWeight.GlobalPosition = _ctx.GlobalPosition;

		// Add spring and weight to scene
		_ctx.CallDeferred(Node.MethodName.AddSibling, _ctx.TongueWeight);
		_ctx.CallDeferred(Node.MethodName.AddSibling, _ctx.TongueSpring);

		// Change state to grapple
		_ctx.StateEnum = Player.State.Grapple;
	}

	public override void DisableGrapple() { }
}
