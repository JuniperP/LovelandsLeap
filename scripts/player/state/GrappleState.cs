using Godot;
using Godot.NativeInterop;

public class GrappleState : IMovementState
{
	private double _grappleTime = 0d;

	public void HandleMovement(Player ctx, double delta)
	{
		_grappleTime += delta;

		// Set velocity to move to weight
		Vector2 diff = ctx.TongueWeight.GlobalPosition - ctx.GlobalPosition;
		ctx.Velocity = diff * 10;

		// If player collided after buffer time
		if (ctx.MoveAndSlide() && _grappleTime >= ctx.AutoDegrappleBuffer)
			DisableGrapple(ctx);
		
		// Handle sprite direction
		if (ctx.Velocity.X > 0.01f)
			ctx.AnimManager.IsLeftFacing = false;
		else if (ctx.Velocity.X < -0.01f)
			ctx.AnimManager.IsLeftFacing = true;
	}

	public void HandleAction(Player ctx)
	{
		if (Input.IsActionJustPressed("move_up") ||
			Input.IsActionJustPressed("move_down") ||
			Input.IsActionJustPressed("primary_click"))
		{
			DisableGrapple(ctx);
		}
	}

	public void EnableGrapple(Player ctx) { }

	public void DisableGrapple(Player ctx)
	{
		// Remove all grappling objects
		ctx.TongueLine.QueueFree();
		ctx.TongueSpring.QueueFree();
		ctx.TongueWeight.QueueFree();

		// Change state to walk
		ctx.MovementState = new WalkState();
		ctx.StateEnum = Player.State.Walk;
	}
}
