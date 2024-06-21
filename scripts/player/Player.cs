using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public enum State : byte
	{
		Walk,
		Grapple
	}

	[Export] public int Speed = 300;
	[Export] public int Acceleration = 2000;
	[Export] public int JumpImpulse = 1000;
	[Export] public float JumpCutFactor = 0.5f;
	[Export] public float GravityMultiplier = 1;
	[Export] public int MaxFallSpeed = 1500;
	[Export] public int TongueProjSpeed = 800;
	[Export] public int TongueAngle = 15;

	// Refactor states to only be created once and switch with Player methods
	public State StateEnum = State.Walk;
	public IMovementState MovementState = new WalkState();
	public bool TongueProjExists = false; // TODO: Refactor into singleton

	// TODO: Refactor into sibling nodes that are disabled
	public PackedScene TongueProjScene;
	public TongueProjectile TongueProj;
	public PackedScene TongueLineScene;
	public TongueLine TongueLine;
	public PackedScene TongueSpringScene;
	public TongueSpring TongueSpring;
	public PackedScene TongueWeightScene;
	public RigidBody2D TongueWeight;

	public override void _Ready()
	{
		TongueProjScene = GD.Load<PackedScene>("res://scenes/player/tongue/tongue_projectile.tscn");
		TongueLineScene = GD.Load<PackedScene>("res://scenes/player/tongue/tongue_line.tscn");
		TongueSpringScene = GD.Load<PackedScene>("res://scenes/player/tongue/tongue_spring.tscn");
		TongueWeightScene = GD.Load<PackedScene>("res://scenes/player/tongue/tongue_weight.tscn");
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

	public void DisableGrapple()
	{
		MovementState.DisableGrapple(this);
	}
}
