using Godot;
using System;

public partial class load_game_in : Control
{

	// Sets up the game
	public override void _Ready()
	{
		// Loads in user settings

		// By this method finishing the logo fade in is triggered to begin
	}

	// Used to finish loading when the game is ready
	private void LogoIn()
	{
		// Finish transition with sound effect
		SoundManager.PlaySound(SFX.TongueHit, this);

		// Dramatic pause
		System.Threading.Thread.Sleep(1000);

		// Send to main screen
		GetTree().ChangeSceneToFile("res://scenes/ui/main_menu.tscn");
	}
    

}
