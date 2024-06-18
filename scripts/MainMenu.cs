using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Starts the game
	private void _on_start_game_button_pressed()
	{
		//Temporary location for the game starting
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}
	
	// Runs the credits
	private void _on_credit_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/credits.tscn");
	}
	
	// Quits the game from the menu
	private void _quit_game()
	{
		GetTree().Quit();
	}
}
