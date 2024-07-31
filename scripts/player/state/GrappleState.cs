using Godot;

public class GrappleState : MovementState
{
	private double _grappleTime = 0d;

	public GrappleState(Player ctx) : base(ctx) { }

	public override void HandleMovement(double delta)
	{
		_grappleTime += delta;

		Vector2 weightPos = Ctx.TongueWeight.GlobalPosition;
		Vector2 springPos = Ctx.TongueSpring.GlobalPosition;

		// Handle horizontal input
		float inputDir = Input.GetAxis("move_left", "move_right");
		// If trying to move and below the tongue pivot
		if (inputDir != 0f && weightPos.Y > springPos.Y)
		{
			// Rotate a 2D vector clockwise or counterclockwise depending on input
			// TODO: Explain this better because vectors are confusing
			// The forceDir will now be tangent to the swing curve of the weight
			Vector2 forceDir = (springPos - weightPos).Normalized();
			forceDir = new Vector2(forceDir.Y, forceDir.X);
			forceDir *= new Vector2(-inputDir, inputDir);

			// Scale force with distance from pivot logarithmically
			float distanceRatio = weightPos.DistanceTo(springPos) / Ctx.SwingBaseDistance;
			distanceRatio = Mathf.Max(1f, distanceRatio);
			float logFactor = Mathf.Log(distanceRatio + Ctx.SwingLogBase - 1)
							/ Mathf.Log(Ctx.SwingLogBase);

			Ctx.TongueWeight.ApplyForce(forceDir * logFactor * Ctx.SwingForce * (float)delta);
		}

		// Set velocity to move to weight
		Vector2 diff = weightPos - Ctx.GlobalPosition;
		Ctx.Velocity = diff * 100;

		// If player collided after buffer time
		if (Ctx.MoveAndSlide() && _grappleTime >= Ctx.AutoDegrappleBuffer)
			DisableGrapple();

		// Handle sprite direction
		if (Ctx.Velocity.X > 0.01f)
			Ctx.AnimManager.IsLeftFacing = false;
		else if (Ctx.Velocity.X < -0.01f)
			Ctx.AnimManager.IsLeftFacing = true;
	}

	public override void HandleAction()
	{
		if (Input.IsActionJustPressed("primary_action"))
			DisableGrapple();
	}

	public override void EnableGrapple() { }

	public override void DisableGrapple()
	{
		// Remove all grappling objects
		Ctx.TongueLine.QueueFree();
		Ctx.TongueSpring.QueueFree();
		Ctx.TongueWeight.QueueFree();

		// Change state to walk
		Ctx.StateEnum = Player.State.Walk;

		// Set animation to idle
		Ctx.AnimManager.State = AnimState.Idle;

		_grappleTime = 0d;
	}
}
