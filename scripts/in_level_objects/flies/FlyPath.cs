using Godot;

public partial class FlyPath : Path2D
{
	// Tracks direction fly should be going
	private bool _goFoward;

	// How the path is being followed
	[Export] public PathFollow2D HowToFollow;
	[Export] public FlyBehavoir Fly;

	[ExportGroup("PropertiesOfTheTrack")]
	[Export] public float TrackSpeed = 5;
	[Export] public bool TrackLoops = false;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_goFoward = true;

		if (TrackLoops == false)
			HowToFollow.Loop = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (TrackLoops || _goFoward)
		{
			HowToFollow.ProgressRatio += (float)delta / 10 * TrackSpeed;

			if (!TrackLoops && HowToFollow.ProgressRatio == 1f)
				ToggleDirection();
		}
		else
		{
			HowToFollow.ProgressRatio -= (float)delta / 10 * TrackSpeed;

			if (HowToFollow.ProgressRatio == 0f)
				ToggleDirection();
		}

	}


	private void ToggleDirection()
	{
		_goFoward = !_goFoward;
		Fly.FlipFly();
	}
}
