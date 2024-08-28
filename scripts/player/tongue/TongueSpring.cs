using Godot;

public partial class TongueSpring : DampedSpringJoint2D
{
	[Export] public float LengthFactor = 1;
	[Export] public PhysicsBody2D Target;

	// Idk why Juniper exports all of these but sure
	[Export] public Vector2 Offset;
	[Export] public Node2D AttachedTo;

	public override void _Ready()
	{
		NodeB = Target.GetPath(); // Set secondary spring node to the player

		LookAt(Target.GlobalPosition);
		// Correct LookAt angle
		Rotate(-0.5f * Mathf.Pi); // NECESSARY, but no clue why :3
		Length = LengthFactor * GlobalPosition.DistanceTo(Target.GlobalPosition);
	}

	// Updating the springs position to match it's surface's
    public override void _Process(double delta)
    {
		if(AttachedTo.IsValid() && AttachedTo is not TileMap)	
        	Position = AttachedTo.GlobalPosition - Offset;
    }
}
