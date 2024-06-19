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
	[Export] public int TongueAngle = 15;

	public IMovementState MovementState = new WalkingState();
	public bool TongueProjExists = false;  // TODO: Refactor into singleton
	public PackedScene TongueProjScene;
	public RigidBody2D TongueProj;
	public PackedScene TongueLineScene;
	public TongueLine TongueLine;
	public PackedScene TongueSpringScene;
	public TongueSpring TongueSpring;

	public override void _Ready()
	{
		TongueProjScene = GD.Load<PackedScene>("res://scenes/tongue_projectile.tscn");
		TongueLineScene = GD.Load<PackedScene>("res://scenes/tongue_line.tscn");
		TongueSpringScene = GD.Load<PackedScene>("res://scenes/tongue_spring.tscn");
	}

	public override void _PhysicsProcess(double delta)
	{
		MovementState.HandleMovement(this, delta);
		MovementState.HandleAction(this);
	}

	public void EnableGrapple(Node target)
	{
		MovementState.EnableGrapple(this);
	}
}
