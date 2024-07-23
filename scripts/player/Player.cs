using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public enum State : byte
	{
		Walk,
		Grapple,
	}

	// Inspector variables
	[ExportGroup("Horizontal")]
	[Export] public int Speed = 300;
	[Export] public int Acceleration = 2000;
	[ExportSubgroup("Acceleration Factors", "Accel")]
	[Export] public float AccelAirFactor = 0.7f;
	[Export] public float AccelOppositionFactor = 1.5f;
	[Export] public float AccelSpeedingFactor = 0.25f;
	[Export] public float AccelSpeedingBase = 1.5f;

	[ExportGroup("Vertical")]
	[Export] public int JumpImpulse = 1000;
	[Export] public float JumpCutFactor = 0.5f;
	[Export] public float GravityFactor = 1f;
	[Export] public float FallGravityFactor = 1.5f;
	[Export] public int MaxFallSpeed = 1500;
	[Export] public float FastFallFactor = 2f;
	[Export] public float FastFallMaxFactor = 2f;

	[ExportGroup("Tongue")]
	[Export] public float TongueOffset = -30f;
	[Export] public int TongueAngle = 15;
	[Export] public double AutoDegrappleBuffer = 0.5;
	[ExportSubgroup("Swinging Factors", "Swing")]
	[Export] public int SwingForce = 6000;
	[Export] public int SwingBaseDistance = 100;
	[Export] public float SwingLogBase = 3;

	[ExportGroup("Scenes")]
	[Export] public PackedScene TongueProjScene;
	[Export] public PackedScene TongueLineScene;
	[Export] public PackedScene TongueSpringScene;
	[Export] public PackedScene TongueWeightScene;

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
	private MovementState[] _movementStates;
	public MovementState MovementState
	{
		get { return _movementStates[(int)StateEnum]; }
	}

	public AnimationManager AnimManager;
	public TongueProjectile TongueProj;
	public TongueLine TongueLine;
	public TongueSpring TongueSpring;
	public RigidBody2D TongueWeight;

	public override void _Ready()
	{
		AnimManager = new AnimationManager(this);
		_movementStates = new MovementState[]{
			new WalkState(this),
			new GrappleState(this)
		};
	}

	public override void _PhysicsProcess(double delta)
	{
		MovementState.HandleMovement(delta);
		MovementState.HandleAction();
	}

	public void EnableGrapple(Node target)
	{
		MovementState.EnableGrapple();
	}

	public void DisableGrapple()
	{
		MovementState.DisableGrapple();
	}
}
