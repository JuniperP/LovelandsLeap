using Godot;

/*
First track in a rail way system.
This track will outline the properties by which follower tracks will obey.

Note: If you want a single line back and forth, use this track as it can
bounce off endings of the track.
*/
public partial class LeaderTrack : Track
{
	// Helper bool for tracks that require bouncing
	private bool _towardA;

	[Export] public PackedScene SceneOnTrack;

	[ExportGroup("PropertiesOfTheTrack")]
	[Export] public float TrackSpeed = 1;
	[Export] public bool Bounce = false;
	[Export] public bool AutoStart = true;



	protected override void FurtherSetup()
	{
		// The the start of the system thus creates the scene to use
		InstanScene = SceneOnTrack.Instantiate<Node2D>();
		AddChild(InstanScene);

		// Starting the user at the beginning of the track
		InstanScene.Position = LineToFollow.A;

		_towardA = false;
		Move = AutoStart;
		SpeedMod = TrackSpeed;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (InstanScene.IsValid() && Move)
		{
			float nodeSpeed = SpeedMod * 100 * (float)delta;

			if (Bounce)
				MoveBounce(nodeSpeed);

			else
				MoveNoBounce(nodeSpeed);
		}
	}

	// Used to move if their is bounce
	private void MoveBounce(float nodeSpeed)
	{
		// Going the right direction
		if (_towardA)
			InstanScene.Position = InstanScene.Position.MoveToward(LineToFollow.A, nodeSpeed);
		else
			InstanScene.Position = InstanScene.Position.MoveToward(LineToFollow.B, nodeSpeed);

		// Flipping direction once a side has been hit
		if (InstanScene.Position == LineToFollow.A)
			_towardA = false;
		else if (InstanScene.Position == LineToFollow.B)
			_towardA = true;
	}



}
