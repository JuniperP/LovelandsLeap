using Godot;

public class GrappleState : MovementState
{
	private double _grappleTime = 0d;

	public GrappleState(Player ctx) : base(ctx) { }

	public override void HandleMovement(double delta)
	{
		_grappleTime += delta;

		// Set velocity to move to weight
		Vector2 diff = _ctx.TongueWeight.GlobalPosition - _ctx.GlobalPosition;
		_ctx.Velocity = diff * 10;

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
		if (Input.IsActionJustPressed("move_up") ||
			Input.IsActionJustPressed("move_down") ||
			Input.IsActionJustPressed("primary_click"))
		{
			DisableGrapple();
		}
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
	}
}
