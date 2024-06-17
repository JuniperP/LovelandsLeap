using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int Speed = 300;
	[Export] public int Acceleration = 2000;
	[Export] public int JumpImpulse = 1000;
	[Export] public float GravityMultiplier = 1;
	[Export] public int MaxFallSpeed = 1500;

	private float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
	private Vector2 _targetVelocity = Vector2.Zero;

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
	}

	private void HandleMovement(double delta)
	{
		// Kill vertical velocity when hitting ceiling
		if (IsOnCeiling())
			_targetVelocity.Y = 0;

		// Handle gravity
		if (!IsOnFloor())
			_targetVelocity.Y += gravity * GravityMultiplier * (float)delta;

		// Handle jump
		else if (Input.IsActionJustPressed("move_up"))
			_targetVelocity.Y = -JumpImpulse;

		// Smooth velocity towards horizontal direction
		float direction = Input.GetAxis("move_left", "move_right");
		_targetVelocity.X = Mathf.MoveToward(
			Velocity.X,
			direction * Speed,
			Acceleration * (float)delta
		);

		// Cap vertical speed
		_targetVelocity.Y = Mathf.Min(_targetVelocity.Y, MaxFallSpeed);

		// Update velocity and move
		Velocity = _targetVelocity;
		MoveAndSlide();
	}
}
