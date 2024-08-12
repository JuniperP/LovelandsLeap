using Godot;
using System;

/*
First track in a rail way system.
This track will outline the properties by which follower tracks will obey.
*/
public partial class LeaderTrack : Area2D
{
	[Export] public CollisionShape2D HitBox;

	// The track for our object
	private SegmentShape2D _lineToFollow;

	[Export] public PackedScene SceneOnTrack;

	// What will ride the railway
	private Node2D _instanScene;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Using our hitbox to get the line our object will follow
		_lineToFollow = (SegmentShape2D)HitBox.Shape;

		_instanScene = SceneOnTrack.Instantiate<Node2D>();
		AddChild(_instanScene);

		// Starting the user at the beginning of the track
		_instanScene.Position = _lineToFollow.A;


	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_instanScene.Position = _instanScene.Position.MoveToward(_lineToFollow.B, (float)delta*100);

	}



}
