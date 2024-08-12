using Godot;

public partial class FollowerTrack : Area2D
{
	// The track for our object
	private SegmentShape2D _lineToFollow;

	private float _trackSpeed;

	// What will ride the railway
	private Node2D _instanScene;

	// Helper bool to say if the item on the track should be moving
	private bool _move;

	[Signal] public delegate void ReachedEndEventHandler(Node2D node, int trackSpeed);

	[Export] public CollisionShape2D HitBox;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Using our hitbox to get the line our object will follow
		_lineToFollow = (SegmentShape2D)HitBox.Shape;

		_instanScene = null;
		_move = false;
	}

	// Starts up the track once it is passed the node by signals
	private void StartTrack(Node2D node, int trackSpeed)
	{
		_trackSpeed = trackSpeed;
		node.Position = _lineToFollow.A;
		_instanScene = node;
		_move = true;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_instanScene.IsValid() && _move)
		{
			float nodeSpeed = _trackSpeed * 100 * (float)delta;

			_instanScene.Position = _instanScene.Position.MoveToward(_lineToFollow.B, nodeSpeed);

			// Passing the scene if the end is reached
			if (_instanScene.Position == _lineToFollow.B)
			{
				EmitSignal(SignalName.ReachedEnd, _instanScene, _trackSpeed);
				_move = false;
			}

		}
	}
}
