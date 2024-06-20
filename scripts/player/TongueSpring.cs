using Godot;
using System;

public partial class TongueSpring : DampedSpringJoint2D
{
	[Export] public PhysicsBody2D Target;

	public override void _Ready()
	{
		NodeB = Target.GetPath(); // Set secondary spring node to target

		LookAt(Target.GlobalPosition);
		Rotate((float)-0.5f * Mathf.Pi); // NECESSARY, but no clue why :3
		Length = GlobalPosition.DistanceTo(Target.GlobalPosition);
	}
}
