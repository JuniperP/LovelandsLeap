using Godot;

/*
First track in a rail way system.
This track will outline the properties by which follower tracks will obey.
*/
public partial class LeaderTrack : Area2D
{

	// The track for our object
	private SegmentShape2D _lineToFollow;

	// What will ride the railway
	private Node2D _instanScene;

	// Helper bool for tracks that require bouncing
	private bool _towardA;

	// Helper bool to say if the item on the track should be moving
	private bool _move;

	[Signal] public delegate void ReachedEndEventHandler(Node2D node, int trackSpeed);


	[ExportGroup("SetUp")]
	[Export] public CollisionShape2D HitBox;
	[Export] public PackedScene SceneOnTrack;

	[ExportGroup("PropertiesOfTheTrack")]
	[Export] public float TrackSpeed = 1;
	[Export] public bool Bounce = false;
	[Export] public bool AutoStart = true;




	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Using our hitbox to get the line our object will follow
		_lineToFollow = (SegmentShape2D)HitBox.Shape;

		_instanScene = SceneOnTrack.Instantiate<Node2D>();
		AddChild(_instanScene);

		// Starting the user at the beginning of the track
		_instanScene.Position = _lineToFollow.A;

		_towardA = false;
		_move = AutoStart;

	}

	private void ToggleTrack()
	{
		_move = !_move;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_instanScene.IsValid() && _move)
		{
			float nodeSpeed = TrackSpeed * 100 * (float)delta;

			if (Bounce)
			{
				// Going the right direction
				if (_towardA)
					_instanScene.Position = _instanScene.Position.MoveToward(_lineToFollow.A, nodeSpeed);
				else
					_instanScene.Position = _instanScene.Position.MoveToward(_lineToFollow.B, nodeSpeed);

				// Flipping direction once a side has been hit
				if (_instanScene.Position == _lineToFollow.A)
					_towardA = false;
				else if (_instanScene.Position == _lineToFollow.B)
					_towardA = true;
			}

			else
			{
				_instanScene.Position = _instanScene.Position.MoveToward(_lineToFollow.B, nodeSpeed);

				// Passing the scene if the end is reached
				if (_instanScene.Position == _lineToFollow.B)
				{
					EmitSignal(SignalName.ReachedEnd, _instanScene, TrackSpeed);
					_move = false;
				}
					
			}
		}
	}



}
