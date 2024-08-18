using Godot;

public partial class MovingPlatformTrack : Path2D
{
	// Tracks whether or not the platform should be moving
	private bool _move;
	// Tracks direction of the platform
	private bool _goBackward;

	// TODO: See if remote transform can be removed without issue
	[Export] public RemoteTransform2D RemoteTransform;
	[Export] public PathFollow2D HowToFollow;
	[Export] public AnimatableBody2D Platform;

	[ExportGroup("PropertiesOfTheTrack")]
	[Export] public float TrackSpeed = 5;
	[Export] public bool TrackLoops = false;
	[Export] public bool BounceOffEnd = false;
	[Export] public bool AutoStart = false;
	[Export] public bool Stoppable = false;
	[Export] public bool OrientPlatformWithTrack = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Assigning the remote to control the platform
		RemoteTransform.RemotePath = Platform.GetPath();

		// Matching how to follow with client's demands
		HowToFollow.Loop = TrackLoops;
		HowToFollow.Rotates = OrientPlatformWithTrack;

		// Starting process if applicable
		_move = AutoStart;
		_goBackward = true;
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
			if (BounceOffEnd && _goBackward)
				HowToFollow.ProgressRatio -= (float)delta / 10 * TrackSpeed;
			else
				HowToFollow.ProgressRatio += (float)delta / 10 * TrackSpeed;

			if (BounceOffEnd && (HowToFollow.ProgressRatio <= 0 || HowToFollow.ProgressRatio >= 1))
				_goBackward = !_goBackward;

		}

	}

}
