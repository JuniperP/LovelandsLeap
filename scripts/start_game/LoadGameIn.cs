using Godot;
using System;

public partial class LoadGameIn : Control
{

	// Sets up the game
	public override void _Ready()
	{
		// Set up for easy default settings
		LoadSettingsData.SetUpDefault();

		// Loads in user settings
		LoadSettingsData.LoadData(false);

		// By this method finishing the logo fade in is triggered to begin
	}

	// Used to finish loading when the game is ready
	private void LogoIn()
	{
		// Send to main screen
		GetTree().ChangeSceneToFile("res://scenes/ui/main_menu.tscn");
	}
}
