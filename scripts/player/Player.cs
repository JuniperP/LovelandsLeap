using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public enum State : byte
	{
		Walk,
		Grapple,
		Max = 1,
	}

	// Inspector variables
	[Export] public int Speed = 300;
	[Export] public int Acceleration = 2000;
	[Export] public int JumpImpulse = 1000;
	[Export] public float JumpCutFactor = 0.5f;
	[Export] public float GravityMultiplier = 1f;
	[Export] public int MaxFallSpeed = 1500;
	[Export] public int TongueProjSpeed = 800;
	[Export] public int TongueAngle = 15;
	[Export] public double AutoDegrappleBuffer = 0.5;
	[Export] public float TongueOffset = -30f;

	// Internal calculation for where the tongue originates
	public Vector2 TongueGlobalPos
	{
		get
		{
			return new Vector2(GlobalPosition.X, GlobalPosition.Y + TongueOffset);
		}
	}

	// Movement state chooses from an array of states using the enum
	public State StateEnum = State.Walk;
	private readonly IMovementState[] _movementStates = {
		new WalkState(),
		new GrappleState()
	};
	public IMovementState MovementState
	{
		get { return _movementStates[(int)StateEnum]; }
	}

	public bool TongueProjExists = false; // TODO: Refactor into singleton
	public AnimationManager AnimManager;

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
		AnimManager = new AnimationManager(this);

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
