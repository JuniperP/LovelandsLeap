// TODO: Refactor into abstract class that has ctx variable
public interface IMovementState
{
    void HandleMovement(Player ctx, double delta);
    void HandleAction(Player ctx);
    void EnableGrapple(Player ctx);
    void DisableGrapple(Player ctx);
}
