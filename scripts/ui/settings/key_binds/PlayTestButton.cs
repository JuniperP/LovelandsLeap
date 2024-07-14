using Godot;
using System;

public partial class PlayTestButton : Button
{
	// What are play tets will assign
	private string _goTo = "";

	// Telling the node that its time to play test
	private void PlayTest()
	{
		_goTo = SceneManager.GetPath(ToScene.PlayTestLevel);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// If asked, the scene switches
		if (!_goTo.Equals(""))
			GetTree().ChangeSceneToFile(_goTo);

	}
}
