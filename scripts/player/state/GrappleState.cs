using Godot;

public class GrappleState : MovementState
{
	private double _grappleTime = 0d;

	public GrappleState(Player ctx) : base(ctx) { }

	public override void HandleMovement(double delta)
	{
		_grappleTime += delta;

		Vector2 weightPos = _ctx.TongueWeight.GlobalPosition;
		Vector2 springPos = _ctx.TongueSpring.GlobalPosition;

		// Handle horizontal input
		float inputDir = Input.GetAxis("move_left", "move_right");
		if (inputDir != 0f)
		{
			if (weightPos.Y > springPos.Y) // Flip rotation if above spring
				inputDir *= -1;

			// Rotate a 2D vector clockwise or counterclockwise depending on input
			// TODO: Explain this better because vectors are confusing
			Vector2 forceDir = (springPos - weightPos).Normalized();
			forceDir = new Vector2(forceDir.Y, forceDir.X);
			forceDir *= new Vector2(inputDir, -inputDir);

			// The forceDir will now be tangent to the swing curve of the weight
			_ctx.TongueWeight.ApplyForce(forceDir * _ctx.SwingForce * (float)delta);
		}

		// Set velocity to move to weight
		Vector2 diff = weightPos - _ctx.GlobalPosition;
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
