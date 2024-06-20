using Godot;

public class GrappleState : IMovementState
{
    public void HandleMovement(Player ctx, double delta)
    {
        // Set velocity to move to weight
        Vector2 diff = ctx.TongueWeight.GlobalPosition - ctx.GlobalPosition;
        ctx.Velocity = diff * 10;
        
        if (ctx.MoveAndSlide()) // If player floored
            DisableGrapple(ctx);
    }

	public void HandleAction(Player ctx) { }

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
