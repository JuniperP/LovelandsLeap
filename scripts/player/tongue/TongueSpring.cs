using Godot;

public partial class TongueSpring : DampedSpringJoint2D
{
	[Export] public float LengthFactor = 1;
	[Export] public PhysicsBody2D Target;

	public override void _Ready()
	{
		NodeB = Target.GetPath(); // Set secondary spring node to target

		LookAt(Target.GlobalPosition);
		Rotate(-0.5f * Mathf.Pi); // NECESSARY, but no clue why :3
		Length = LengthFactor * GlobalPosition.DistanceTo(Target.GlobalPosition);
	}
}
