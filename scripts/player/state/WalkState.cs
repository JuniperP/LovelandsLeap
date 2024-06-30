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

		// Handle gravity
		float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
		if (floored)
		{
			_fastFall = false;

			// Handle jump
			if (Input.IsActionJustPressed("move_up"))
				targetVelocity.Y = -_ctx.JumpImpulse;
		}
		else
		{
			if (Input.IsActionJustPressed("move_down"))
				_fastFall = true;

			float velIncrease = gravity * _ctx.GravityMultiplier * (float)delta;
			if (_fastFall)
				velIncrease *= _ctx.FastFallMultiplier;
			targetVelocity.Y += velIncrease;

			// Display jumping animation unless shooting tongue 
			if (newState != AnimState.Tongue)
				newState = AnimState.Jumping;
		}

		// Handle jump cut
		if (Input.IsActionJustReleased("move_up") && targetVelocity.Y < -0.1f)
			targetVelocity.Y *= _ctx.JumpCutFactor;

		// Cap vertical speed
		float maxFall = _ctx.MaxFallSpeed;
		if (_fastFall)
			maxFall *= _ctx.FastFallMaxMultiplier;
		targetVelocity.Y = Mathf.Min(targetVelocity.Y, maxFall);

		// Smooth velocity towards horizontal direction
		float direction = Input.GetAxis("move_left", "move_right");
		targetVelocity.X = Mathf.MoveToward(
			_ctx.Velocity.X,
			direction * _ctx.Speed,
			_ctx.Acceleration * (float)delta
		);

		// Update velocity and move
		_ctx.Velocity = targetVelocity;
		_ctx.MoveAndSlide();

		// Handle sprite direction
		if (direction > 0.01f)
			_ctx.AnimManager.IsLeftFacing = false;
		else if (direction < -0.01f)
			_ctx.AnimManager.IsLeftFacing = true;

		// Set animation state
		if (floored && newState != AnimState.Tongue)
			newState = Mathf.Abs(direction) > 0.01f
			? AnimState.Walking : AnimState.Idle;
		_ctx.AnimManager.State = newState;
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
			Vector2 direction = (mousePos - _ctx.Position).Normalized();

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

			// Move projectile towards mouse position
			_ctx.TongueProj.LinearVelocity = direction * _ctx.TongueProjSpeed;

			// Setup projectile
			_ctx.TongueProj.BodyEntered += _ctx.EnableGrapple;
			_ctx.AddChild(_ctx.TongueProj);
			_ctx.TongueProj.GlobalPosition = _ctx.TongueGlobalPos;

			// Create and setup tongue line
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
