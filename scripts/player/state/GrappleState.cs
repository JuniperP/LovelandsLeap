using Godot;

public class GrappleState : MovementState
{
	private double _grappleTime = 0d;

	public GrappleState(Player ctx) : base(ctx) { }

	public override void HandleMovement(double delta)
	{
		_grappleTime += delta;

		float inputDir = Input.GetAxis("move_left", "move_right");
		if (inputDir != 0f)
		{
			Vector2 currentDir = _ctx.TongueWeight.LinearVelocity.Normalized();
			float opposite = Mathf.Sign(currentDir.X) * Mathf.Sign(inputDir);
			_ctx.TongueWeight.ApplyForce(opposite * currentDir * _ctx.SwingForce);
		}

		// Set velocity to move to weight
		Vector2 diff = _ctx.TongueWeight.GlobalPosition - _ctx.GlobalPosition;
		_ctx.Velocity = diff * 100;

		// If player collided after buffer time
		if (_ctx.MoveAndSlide() && _grappleTime >= _ctx.AutoDegrappleBuffer)
			DisableGrapple();

		// Handle sprite direction
		if (_ctx.Velocity.X > 0.01f)
			_ctx.AnimManager.IsLeftFacing = false;
		else if (_ctx.Velocity.X < -0.01f)
			_ctx.AnimManager.IsLeftFacing = true;
	}

	public override void HandleAction()
	{
		if (Input.IsActionJustPressed("primary_click"))
			DisableGrapple();
	}

	public override void EnableGrapple() { }

	public override void DisableGrapple()
	{
		// Remove all grappling objects
		_ctx.TongueLine.QueueFree();
		_ctx.TongueSpring.QueueFree();
		_ctx.TongueWeight.QueueFree();

		// Change state to walk
		_ctx.StateEnum = Player.State.Walk;

		// Set animation to idle
		_ctx.AnimManager.State = AnimState.Idle;

		_grappleTime = 0d;
	}
}
