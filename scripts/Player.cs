using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int Speed = 300;
	[Export] public int JumpImpulse = 20;
	private float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
	private Vector2 _targetVelocity = Vector2.Zero;

	public override void _PhysicsProcess(double delta)
	{
		handleMovement(delta);
		Velocity = _targetVelocity;
		MoveAndSlide();
	}

	private void handleMovement(double delta)
	{
		// Handle gravity
		if (!IsOnFloor())
			_targetVelocity.Y += gravity * (float)delta;

		Vector2 direction = Vector2.Zero;
		
		if (Input.IsActionPressed("move_right"))
			direction.X += 1;
		
		if (Input.IsActionPressed("move_left"))
			direction.X -= 1;
		
		direction = direction.Normalized() * Speed;
		Position += direction * (float)delta;
	}
}
