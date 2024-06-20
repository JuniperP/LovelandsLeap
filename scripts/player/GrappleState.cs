using Godot;

public class GrappleState : IMovementState
{
    public void HandleMovement(Player ctx, double delta)
    {
        // Set velocity to move to weight
        Vector2 diff = ctx.TongueWeight.GlobalPosition - ctx.GlobalPosition;
        ctx.Velocity = diff * 10;

    }

    public void HandleAction(Player ctx)
    {

    }

    public void EnableGrapple(Player ctx)
    {

    }
}
