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
		HandleMovement(delta);
		Velocity = _targetVelocity;
		MoveAndSlide();
	}

	private void HandleMovement(double delta)
	{
		// Handle gravity
		if (!IsOnFloor())
			_targetVelocity.Y += gravity * (float)delta;

		// Handle jump
		if (Input.IsActionJustPressed("move_up") && IsOnFloor())
		{
			// TODO: Implement jump
		}

		// TODO: Change to float
		Vector2 direction = Vector2.Zero;

		if (Input.IsActionPressed("move_right"))
			direction.X += 1;

		if (Input.IsActionPressed("move_left"))
			direction.X -= 1;

		_targetVelocity.X = direction.X * Speed;
	}
}
