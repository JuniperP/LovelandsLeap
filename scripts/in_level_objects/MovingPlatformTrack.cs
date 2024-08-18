using Godot;

public partial class MovingPlatformTrack : Path2D
{
	// Tracks whether or not the platform should be moving
	private bool _move;


	[Export] public RemoteTransform2D RemoteTransform;
	[Export] public PathFollow2D HowToFollow;
	[Export] public AnimatableBody2D Platform;

	[ExportGroup("PropertiesOfTheTrack")]
	[Export] public float TrackSpeed = 5;
	[Export] public bool TrackLoops = false;
	[Export] public bool BounceOffEnd = false;
	[Export] public bool AutoStart = false;
	[Export] public bool Stoppable = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Assigning the remote to control the platform
		RemoteTransform.RemotePath = Platform.GetPath();

		// Matching how to follow with client's demands
		HowToFollow.Loop = TrackLoops;

		// Starting process if applicable
		_move = AutoStart;
	}

	// Easy signal transfers to stop / start the platform
	private void TogglePlatform()
	{
		if ((_move && Stoppable) || !_move)
			_move = !_move;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_move)
		{
			HowToFollow.ProgressRatio += (float)delta / 10 * TrackSpeed;
		}

	}

}
