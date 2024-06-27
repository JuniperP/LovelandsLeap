public abstract class MovementState
{
	protected Player _ctx;
	public MovementState(Player ctx)
	{
		_ctx = ctx;
	}
	
	public abstract void HandleMovement(double delta);
	public abstract void HandleAction();
	public abstract void EnableGrapple();
	public abstract void DisableGrapple();
}
