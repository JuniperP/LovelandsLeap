using Godot;

public partial class MovingPlatformTrack : Path2D
{
	[Export] public RemoteTransform2D RemoteTransform;
	[Export] public PathFollow2D HowToFollow;
	[Export] public AnimatableBody2D Platform;

	[ExportGroup("PropertiesOfTheTrack")]
	[Export] public float TrackSpeed = 5;
	[Export] public bool TrackLoops = false;
	[Export] public bool OrientPlatformWithTrack = false;
	[Export] public bool BounceOffEnd = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Assigning the remote to control the platform
		RemoteTransform.RemotePath = Platform.GetPath();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		HowToFollow.ProgressRatio += (float) delta;
	}

}
