using Godot;
using System;

public partial class FlyBehavoir : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Match level count to fly assets used
		FlyCount.TotalLevelFlies++;
	}
}
