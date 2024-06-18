using Godot;
using System;

public partial class pause_screen : Control
{
	// Setup
	public override void _Ready()
	{
		Visible=false;
	}
	
	// Close the menu
	private void _close()
	{
		Visible=false;
		GetTree().Paused=false;
	}

	// Open the menu
	private void _open()
	{
		Visible=true;
		GetTree().Paused=true;
	}

	// Return to the main menu
	private void _to_main_menu()
	{
		GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Sees if the user is trying to pause the game
		if (Input.IsActionPressed("ui_cancel"))
		{
			_open();
		}
	}
}

