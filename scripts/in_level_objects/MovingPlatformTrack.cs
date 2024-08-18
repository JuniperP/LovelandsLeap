using Godot;

public partial class MovingPlatformTrack : Path2D
{
	// Tracks whether or not the platform should be moving
	private bool _move;
	// Tracks direction of the platform
	private bool _goBackward;

	private float _slowDownFactor;

	[Export] public RemoteTransform2D RemoteTransform; // Required less the collision doesn't actually move
	[Export] public PathFollow2D HowToFollow;
	[Export] public AnimatableBody2D Platform;

	// Note: it is not possible to have a looping bouncing track; less it would loop and bounce at the end
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
		_slowDownFactor = 30;
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
			// Constant speed
			float slowDownFactor = CalculateSpeedFactor(delta);

			// Moving the platform the right direction
			if (BounceOffEnd && _goBackward)
				HowToFollow.ProgressRatio -= (float)delta / slowDownFactor * TrackSpeed;
			else
				HowToFollow.ProgressRatio += (float)delta / slowDownFactor * TrackSpeed;

			// Flipping direction if needed
			if (BounceOffEnd && (HowToFollow.ProgressRatio <= 0 || HowToFollow.ProgressRatio >= 1))
				_goBackward = !_goBackward;

		}

	}

	// Calculated the arbitrary speed with easing
	private float CalculateSpeedFactor(double delta)
	{
		// Constant speed
		float ratio = HowToFollow.ProgressRatio;

		// Check basics reqs to ease
		if ((HowToFollow.ProgressRatio <= .1 || HowToFollow.ProgressRatio >= .9) && BounceOffEnd)
		{
			// Speedup when headed toward center
			if (_slowDownFactor > 10 && ((_goBackward && ratio >= .9) || (!_goBackward && ratio <= .1)))
				_slowDownFactor -= (float)delta * 20;

			// Slow down when approaching edge
			if (_slowDownFactor < 30 && ((_goBackward && ratio <= .1) || (_goBackward && ratio >= .9)))
				_slowDownFactor += (float)delta * 20;
		}
		else
			_slowDownFactor = 10;

		return _slowDownFactor;
	}

}
