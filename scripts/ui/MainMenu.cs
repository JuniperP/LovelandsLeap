using Godot;
using System;

public partial class MainMenu : Control
{
	// Main menu music
	private AudioStreamPlayer mus;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Loads in save data
		//SaveGame.LoadData();

		// Getting opening theme
		mus = (AudioStreamPlayer) GetNode("MainMenuTheme");
		mus.Play();
	}

	// Starts the game
	private void _on_start_game_button_pressed()
	{
		mus.Stop();
		//Temporary location for the game starting
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}
	
	// Runs the credits
	private void _on_credit_button_pressed()
	{
		mus.Stop();
		GetTree().ChangeSceneToFile("res://scenes/ui/credits.tscn");
	}
	
	// Quits the game from the menu after saving
	private void _quit_game()
	{
		//SaveGame.Save();
		GetTree().Quit();
	}
}
