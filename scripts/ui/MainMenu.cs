using Godot;
using System;

public partial class MainMenu : Control
{
	// Starts the game
	private void _on_start_game_button_pressed()
	{

		// Temporary location for the game starting
		GetTree().ChangeSceneToFile("res://scenes/cutscene/opening.tscn");
	}

	// Runs the credits
	private void _on_credit_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/ui/credits.tscn");
	}

	// Quits the game from the menu after saving
	private void _quit_game()
	{
		//SaveGame.Save();
		GetTree().Quit();
	}
}
