public interface IMovementState
{
    void HandleMovement(Player ctx, double delta);
    void HandleAction(Player ctx);
    void EnableGrapple(Player ctx);
}
