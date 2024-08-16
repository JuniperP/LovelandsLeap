using Godot;

public partial class Track : Node2D
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

	// The positions on the track
	protected Vector2 PointA;
	protected Vector2 PointB;

	// This nodes leader in the system
	protected LeaderTrack Leader;

	[Signal] public delegate void ReachedEndEventHandler(Node2D node, int trackSpeed, bool Orient, LeaderTrack leader);

	[Export] public CollisionShape2D HitBox;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Using our hitbox to get the line our object will follow
		LineToFollow = (SegmentShape2D)HitBox.Shape;

		// Setting up where we wish to go accounting for parents' areas' offsets
		PointA = LineToFollow.A + HitBox.Position;
		PointB = LineToFollow.B + HitBox.Position;

		FurtherSetup();
	}

	// Starts up the track once it is passed the node by signals
	protected void StartTrack(Node2D node, int trackSpeed, bool orient, LeaderTrack leader)
	{
		SpeedMod = trackSpeed;
		node.Position = PointA;
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
		AlignScene(PointA.AngleToPoint(PointB));

		InstanScene.Position = InstanScene.Position.MoveToward(PointB, nodeSpeed);

		// Passing the scene if the end is reached
		if (InstanScene.Position == PointB)
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

	// Potential way of doing this if I was lazy and just made copies of the tile map for every platform
	/*
	protected void AdjustPlatformVelocity()
	{
		if (InstanScene is TileMap)
		{
			TileMap scene = (TileMap)InstanScene;
			Godot.Collections.Array<Vector2I> usedCells = scene.GetUsedCells(0);

			for (int i = 0; i < usedCells.Count; i++)
			{
				scene.GetCellTileData(0, usedCells[i]).SetConstantLinearVelocity(0, new Vector2(1000, -1000));
			}
		}
	}
*/
}
