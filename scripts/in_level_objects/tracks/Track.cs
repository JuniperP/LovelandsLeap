using Godot;

public partial class Track : Node
{
	//TODO:
	//Orient towards direction going option
	//Stop all aspects of the track and followers options

	// The track for our object
	protected SegmentShape2D LineToFollow;

	// What will ride the railway
	protected Node2D InstanScene;

	// How fast the track shall go
	protected float SpeedMod;

	// Helper bool to say if the item on the track should be moving
	protected bool Move;

	[Signal] public delegate void ReachedEndEventHandler(Node2D node, int trackSpeed);

	[Export] public CollisionShape2D HitBox;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Using our hitbox to get the line our object will follow
		LineToFollow = (SegmentShape2D)HitBox.Shape;

		FurtherSetup();
	}

	// Starts up the track once it is passed the node by signals
	protected void StartTrack(Node2D node, int trackSpeed)
	{
		SpeedMod = trackSpeed;
		node.Position = LineToFollow.A;
		InstanScene = node;
		Move = true;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (InstanScene.IsValid() && Move)
			MoveNoBounce(SpeedMod * 100 * (float)delta);
	}


	// Used to move if their is no bounce 
	protected void MoveNoBounce(float nodeSpeed)
	{
		InstanScene.Position = InstanScene.Position.MoveToward(LineToFollow.B, nodeSpeed);

		// Passing the scene if the end is reached
		if (InstanScene.Position == LineToFollow.B)
		{
			EmitSignal(SignalName.ReachedEnd, InstanScene, SpeedMod);
			Move = false;
		}
	}

	
	// To be used by special types of tracks that require more setup
	protected virtual void FurtherSetup()
	{
		InstanScene = null;
		Move = false;
		SpeedMod = 0;
	}

}
