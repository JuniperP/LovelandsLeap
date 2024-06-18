using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int Speed = 300;
	[Export] public int Acceleration = 2000;
	[Export] public int JumpImpulse = 1000;
	[Export] public float GravityMultiplier = 1;
	[Export] public int MaxFallSpeed = 1500;
	[Export] public int TongueSpeed = 800;

	private float gravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");
	private PackedScene tongueProjScene;
	private RigidBody2D _tongueProj;
	private bool _isGrappling = false;
	private bool _isTongueProj = false;  // TODO: Refactor into singleton

	public override void _Ready()
	{
		tongueProjScene = GD.Load<PackedScene>("res://scenes/tongue_projectile.tscn");
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement(delta);
		HandleAction(delta);
	}

	private void HandleMovement(double delta)
	{
		Vector2 targetVelocity = Velocity;

		// Kill vertical velocity when hitting ceiling
		if (IsOnCeiling())
			targetVelocity.Y = 0;

		// Handle gravity
		if (!IsOnFloor())
			targetVelocity.Y += gravity * GravityMultiplier * (float)delta;

		// Handle jump
		else if (Input.IsActionJustPressed("move_up"))
			targetVelocity.Y = -JumpImpulse;

		// Smooth velocity towards horizontal direction
		float direction = Input.GetAxis("move_left", "move_right");
		targetVelocity.X = Mathf.MoveToward(
			Velocity.X,
			direction * Speed,
			Acceleration * (float)delta
		);

		// Cap vertical speed
		targetVelocity.Y = Mathf.Min(targetVelocity.Y, MaxFallSpeed);

		// Update velocity and move
		Velocity = targetVelocity;
		MoveAndSlide();
	}

	private void HandleAction(double delta)
	{
		if (Input.IsActionJustPressed("primary_click"))
		{
			if (_isTongueProj || _isGrappling) // Skip if using tongue
				return;

			// Create tongue projectile
			_tongueProj = tongueProjScene.Instantiate<RigidBody2D>();
			_isTongueProj = true;

			// Move tongue towards mouse position
			Vector2 mousePos = GetViewport().GetMousePosition();
			Vector2 direction = (mousePos - Position).Normalized();
			_tongueProj.LinearVelocity = direction * TongueSpeed;

			_tongueProj.BodyEntered += EnableGrapple;
			AddChild(_tongueProj);
		}
	}

	private void EnableGrapple(Node target)
	{
		if (_isGrappling) // Skip if already grappling
			return;

		GD.Print("Enabling grapple");
	}
}
