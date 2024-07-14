public abstract class MovementState
{
	protected Player Ctx;
	public MovementState(Player ctx)
	{
		Ctx = ctx;
	}
	
	public abstract void HandleMovement(double delta);
	public abstract void HandleAction();
	public abstract void EnableGrapple();
	public abstract void DisableGrapple();
}
