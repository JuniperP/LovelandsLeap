using Godot;

public abstract class MovementState
{
	protected Player Ctx;
	public MovementState(Player ctx)
	{
		Ctx = ctx;
	}

	// Methods that subclass states must implement
	public abstract void HandleMovement(double delta);
	public abstract void HandleActions();
	public abstract void EnableGrapple(Node2D target);
	public abstract void DisableGrapple();
}
