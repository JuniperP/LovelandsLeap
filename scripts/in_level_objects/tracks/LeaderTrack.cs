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

	// What will rid the railway
	[Export] public PackedScene SceneOnTrack;

	// Given scenes position on the track
	private Vector2 _position;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Using our hitbox to get the line our object will follow
		_lineToFollow = (SegmentShape2D)HitBox.Shape;

		// Starting the user at the beginning of the track
		_position = _lineToFollow.A;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		
	}



}
