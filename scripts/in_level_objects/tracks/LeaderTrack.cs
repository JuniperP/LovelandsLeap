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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Using our hitbox to get the line our object will follow
		_lineToFollow = (SegmentShape2D)HitBox.Shape;
	}



}
