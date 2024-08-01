using Godot;

public partial class TongueProjectile : RigidBody2D
{
	[Export] public int Speed = 1000;
	// The distance at which the projectile starts returning
	[Export] public float MaxDistance = 500;
	// The distance at which the projectile is considered returned
	[Export] public float ReturnDistance = 25;
	[Export] public TongueLine TongueLine;

	private Player _source;
	private float _maxDistanceSqr;
	private float _returnDistanceSqr;
	private bool _isReturning = false;
	private Vector2 _targetVelocity;

	// Sets up the projectile to react to collision and start flying in a direction
	public void Setup(BodyEnteredEventHandler onCollision, Vector2 direction)
	{
		BodyEntered += onCollision;
		_targetVelocity = direction * Speed;
	}

	public override void _Ready()
	{
		_source = GetParent<Player>();
		GlobalPosition = _source.TongueGlobalPos;

		/* 
			Perform an optimization by squaring the distances once,
			rather than square rooting the distance in PhysicsProcess every frame
		 */
		_maxDistanceSqr = Mathf.Pow(MaxDistance, 2);
		_returnDistanceSqr = Mathf.Pow(ReturnDistance, 2);
	}

	public override void _PhysicsProcess(double delta)
	{
		// Vector from the tongue to the source (player)
		Vector2 diff = _source.TongueGlobalPos - GlobalPosition;

		if (_isReturning)
		{
			// Check if projectile is close enough to player
			if (diff.LengthSquared() < _returnDistanceSqr)
			{
				QueueFree();
				TongueLine.QueueFree();
				_source.AnimManager.State = AnimState.Idle;
			}
			// Move the projectile towards the player
			else
				_targetVelocity = diff.Normalized() * Speed * 2;
		}
		// Start retracting the tongue if the projectile is too far
		else if (diff.LengthSquared() > _maxDistanceSqr)
			RetractTongue();
	}

	// Set linear velocity in IntegrateForces because of best practice
	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		state.LinearVelocity = _targetVelocity;
	}

	// Reset speed and ignore collisions while returning
	public void RetractTongue()
	{
		_isReturning = true;
		LinearVelocity = Vector2.Zero;
		CollisionLayer = 0u;
		CollisionMask = 0u;
	}
}
