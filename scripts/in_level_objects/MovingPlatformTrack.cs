using Godot;

public partial class MovingPlatformTrack : Path2D
{
	// Tracks whether or not the platform should be moving
	private bool _move;
	private float _percentChange;

	private float _slowDownFactor;

	[Export] public RemoteTransform2D RemoteTransform; // Required less the collision doesn't actually move
	[Export] public PathFollow2D HowToFollow;
	[Export] public AnimatableBody2D Platform;

	// Note: it is not possible to have a looping bouncing track; less it would loop and bounce at the end
	[ExportGroup("PropertiesOfTheTrack")]
	[Export] public bool TrackLoops = false;
	[Export] public bool BounceOffEnd = false;
	[Export] public bool AutoStart = false;
	[Export] public bool Stoppable = false;
	[Export] public bool OrientPlatformWithTrack = false;
	[Export] public float TimeToCompleteTrack = 1;


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
		// Moving the platform accordingly
		if (_move)
			HowToFollow.ProgressRatio = CalculateOnCurve(HowToFollow.ProgressRatio, delta);

	}

	// Calculated the arbitrary speed with easing
	private float CalculateOnCurve(float ratio, double delta)
	{
		if (BounceOffEnd)
		{
			// Updating the point on the curve
			_percentChange += (float)delta / TimeToCompleteTrack;

			// Moving the point on a curve based on the new % of the to cover
			return -.5f * Mathf.Cos(Mathf.Pi * _percentChange) + .5f;
		}

		return ratio + (float)delta / TimeToCompleteTrack;
	}

}
