using Godot;
using System;

public partial class PlayTestButton : Button
{
	// What are play tets will assign
	private String GoTo = "";

	// Telling the node that its time to play test
	private void _play_test()
	{
		GoTo = "res://scenes/levels/play_test.tscn";

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// If asked, the scene switches
		if (!GoTo.Equals(""))
			GetTree().ChangeSceneToFile(GoTo);

	}
}
