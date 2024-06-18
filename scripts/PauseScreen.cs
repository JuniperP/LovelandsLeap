using Godot;
using System;

public partial class PauseScreen : Control
{
	// Setup
	public override void _Ready()
	{
		Visible = false;
	}

	// Close the menu
	private void _close()
	{
		GetTree().Paused = false;
		Visible = false;
	}

	// Open the menu
	private void _open()
	{
		Visible = true;
		GetTree().Paused = true;
	}

	// Return to the main menu
	private void _to_main_menu()
	{
		_close();
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

