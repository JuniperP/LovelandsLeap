using Godot;

public class GrappleState : MovementState
{
	// How long the player has been grappling
	private double _grappleTime = 0d;

	// Constructor that calls parent constructor (weird notation)
	public GrappleState(Player ctx) : base(ctx) { }

	public override void HandleMovement(double delta)
	{
		_grappleTime += delta;

		// Store as variables for brevity
		Vector2 weightPos = Ctx.TongueWeight.GlobalPosition;
		Vector2 springPos = Ctx.TongueSpring.GlobalPosition;

		// Horizontal input direction in range [-1f, 1f]
		float inputDir = Input.GetAxis("move_left", "move_right");

		// If trying to move and below the tongue pivot
		if (inputDir != 0f && weightPos.Y > springPos.Y)
		{
			// Get the normalized vector from the weight to spring
			Vector2 forceDir = weightPos.DirectionTo(springPos);

			/* 
				Vector arithmetic dictates that swapping the X and Y values and
				multiplying one component by -1 results in 90 degree rotation.

				Which component is multiplied determines the clockwise-ness of the
				rotation: Multiplying the X by -1 results in a clockwise rotation.

				The forceDir will now be tangent to the swing curve of the weight,
				and it will point in the direction of the desired force.
			 */
			forceDir = new Vector2(forceDir.Y, forceDir.X);
			forceDir *= new Vector2(-inputDir, inputDir);

			// Scale force with distance from pivot logarithmically
			float distanceRatio = weightPos.DistanceTo(springPos) / Ctx.SwingBaseDistance;
			distanceRatio = Mathf.Max(1f, distanceRatio);
			float logFactor = Mathf.Log(distanceRatio + Ctx.SwingLogBase - 1)
							/ Mathf.Log(Ctx.SwingLogBase);

			// Apply the logarithmically adjusted force to the weight
			Ctx.TongueWeight.ApplyForce(forceDir * logFactor * Ctx.SwingForce * (float)delta);
		}

		Ctx.TongueWeight.ApplyForce(Ctx.SwingFollowForce * Ctx.TongueSpring.Displacement * (float)delta);

		// Set player velocity to move towards weight
		Vector2 diff = weightPos - Ctx.GlobalPosition;
		Ctx.Velocity = diff * 100;

		// If player collided after buffer time (effectively a grace period)
		if (Ctx.MoveAndSlide() && _grappleTime >= Ctx.AutoDegrappleBuffer)
		{
			SoundManager.PlaySound(SFX.Land, Ctx);
			DisableGrapple();
		}

		// Handle sprite direction
		if (Ctx.Velocity.X > 100f)
			Ctx.AnimManager.IsLeftFacing = false;
		else if (Ctx.Velocity.X < -100f)
			Ctx.AnimManager.IsLeftFacing = true;
	}

	public override void HandleActions()
	{
		if (Input.IsActionJustPressed("secondary_action"))
			DisableGrapple();
	}

	public override void EnableGrapple(Node2D target) { }

	public override void DisableGrapple()
	{
		// Remove all grappling objects
		Ctx.TongueLine.QueueFree();
		Ctx.TongueSpring.QueueFree();
		Ctx.TongueSpringRemote.QueueFree();
		Ctx.TongueWeight.QueueFree();

		// Change state to walk
		Ctx.StateEnum = Player.State.Walk;

		// Set animation to idle
		Ctx.AnimManager.State = AnimState.Idle;

		_grappleTime = 0d;
	}
}
