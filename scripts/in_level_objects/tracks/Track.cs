using Godot;

public partial class Track : Node
{
	// The track for our object
	protected SegmentShape2D LineToFollow;

	// What will ride the railway
	protected Node2D InstanScene;

	// How fast the track shall go
	protected float SpeedMod;

	// Should the item align its top with the track
	protected bool Orient;

	// If the item on the track should be moving
	protected bool Move;

	// This nodes leader in the system
	protected LeaderTrack Leader;

	[Signal] public delegate void ReachedEndEventHandler(Node2D node, int trackSpeed, bool Orient, LeaderTrack leader);

	[Export] public CollisionShape2D HitBox;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Using our hitbox to get the line our object will follow
		LineToFollow = (SegmentShape2D)HitBox.Shape;

		FurtherSetup();
	}

	// Starts up the track once it is passed the node by signals
	protected void StartTrack(Node2D node, int trackSpeed, bool orient, LeaderTrack leader)
	{
		SpeedMod = trackSpeed;
		node.Position = LineToFollow.A;
		InstanScene = node;
		Move = true;
		Orient = orient;
		Leader = leader;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (InstanScene.IsValid() && Move && Leader.MoveSystem)
			MoveNoBounce(SpeedMod * 100 * (float)delta);
	}


	// Used to move if their is no bounce 
	protected void MoveNoBounce(float nodeSpeed)
	{
		// Orienting the scene if needed
		AlignScene(LineToFollow.A.AngleToPoint(LineToFollow.B));

		InstanScene.Position = InstanScene.Position.MoveToward(LineToFollow.B, nodeSpeed);

		// Passing the scene if the end is reached
		if (InstanScene.Position == LineToFollow.B)
		{
			EmitSignal(SignalName.ReachedEnd, InstanScene, SpeedMod, Orient, Leader);
			Move = false;
		}

	}


	// To be used by special types of tracks that require more setup
	protected virtual void FurtherSetup()
	{
		InstanScene = null;
	}


	// Aligns the object on the track towards the direction it is moving
	protected void AlignScene(float angle)
	{
		if (Orient)
			InstanScene.Rotation = angle;

	}

}
