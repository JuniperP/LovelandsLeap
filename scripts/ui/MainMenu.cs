using Godot;
using System;

public partial class MainMenu : Control
{
	// Where we will doing a scene change to
	private String GoTo;

	public override void _Ready()
	{
		// Set up button sfx
		SoundManager.ApplyButtonSFX(this);

		// Ensure scene isn't changed without permission
		GoTo = "";
	}

	// Sets up to start the game
	private void _on_start_game_button_pressed()
	{
		GoTo = "res://scenes/cutscene/opening.tscn";
	}

	// Sets up to run the credits
	private void _on_credit_button_pressed()
	{
		GoTo = "res://scenes/ui/credits.tscn";
	}

	// Quits the game from the menu after saving
	private void _quit_game()
	{
		//SaveGame.Save();
		GetTree().Quit();
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// If asked, the scene switches
		if (!GoTo.Equals(""))
			GetTree().ChangeSceneToFile(GoTo);

	}
}
