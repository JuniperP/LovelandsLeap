using System;
using Godot;

public partial class TongueSpring : DampedSpringJoint2D
{
	[Export] public float LengthFactor = 1;

	public PhysicsBody2D Target;
	public Vector2 Offset;
	public Node2D AttachedTo;
	// Displacement since last physics process
	public Vector2 Displacement { get; private set; } = Vector2.Zero;
	// Vars to ensure no rotation issues
	private float _initialGrapplePlatformRotation = 0f;
	public RemoteTransform2D Remote;


	public override void _Ready()
	{
		NodeB = Target.GetPath(); // Set secondary spring node to the player

		LookAt(Target.GlobalPosition);
		// Correct LookAt angle
		Rotate(-0.5f * Mathf.Pi); // NECESSARY, but no clue why :3
		Length = LengthFactor * GlobalPosition.DistanceTo(Target.GlobalPosition);

		// Setting up remote transforms for rotating platforms
		if (AttachedTo.IsValid() && AttachedTo is not TileMap)
		{
			Remote.RemotePath = GetPath();
			AttachedTo.AddChild(Remote);
			Remote.GlobalPosition = GlobalPosition;
		}

	}

	// Updating the springs position to match it's surface's
	public override void _PhysicsProcess(double delta)
	{
		if (AttachedTo.IsValid() && AttachedTo is not TileMap)
		{
			// Setting the new position
			Vector2 oldPos = GlobalPosition;
			Vector2 NewPosition = Remote.GlobalPosition;
			Displacement = NewPosition - oldPos;
		}
	}
}
