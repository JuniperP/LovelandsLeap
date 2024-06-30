using Godot;
using System;

public partial class TongueProjectile : RigidBody2D
{
	[Export] public int Speed = 1000;
	[Export] public float MaxDistance = 500;
	[Export] public float ReturnDistance = 25;
	[Export] public TongueLine TongueLine;

	private Player _source;
	private float _maxDistanceSqr;
	private bool _isReturning = false;

	public void Setup(BodyEnteredEventHandler onCollision, Vector2 direction)
	{
		BodyEntered += onCollision;
		LinearVelocity = direction * Speed;
	}

	public override void _Ready()
	{
		_source = GetParent<Player>();
		GlobalPosition = _source.TongueGlobalPos;
		_maxDistanceSqr = Mathf.Pow(MaxDistance, 2);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 diff = _source.TongueGlobalPos - GlobalPosition;
		if (_isReturning)
		{
			if (diff.LengthSquared() < Mathf.Pow(ReturnDistance, 2))
			{
				QueueFree();
				TongueLine.QueueFree();
				_source.AnimManager.State = AnimState.Idle;
			}
			else
				// TODO: Not supposed to set this every frame
				LinearVelocity = diff.Normalized() * Speed * 2;
		}
		else if (diff.LengthSquared() > _maxDistanceSqr)
			RetractTongue(diff);
	}

	public void RetractTongue(Vector2 diff)
	{
		_isReturning = true;
		LinearVelocity = Vector2.Zero;
		CollisionLayer = 0u;
		CollisionMask = 0u;
	}
}
