using Godot;
using System;

public partial class TongueSpring : DampedSpringJoint2D
{
	[Export] public PhysicsBody2D Target;

	public override void _Ready()
	{
		NodeB = Target.GetPath(); // Set secondary spring node to target
		
		Vector2 direction = GlobalPosition - Target.GlobalPosition;
		Rotation = direction.Angle();
		Length = direction.Length();
	}
}
