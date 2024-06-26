using Godot;
using System;
//using System.Runtime.CompilerServices;

public partial class PauseScreen : Toggleable
{
	// Effectively the timer we use to ensure a reasonable amount of time passes between our events
	private double counter;

	// Setup
	public override void _Ready()
	{
		Visible = false;
		counter = 0;
	}

	// Return to the main menu
	private void _to_main_menu()
	{
		GetTree().Paused = false;
		_close();
		GetTree().ChangeSceneToFile("res://scenes/ui/main_menu.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Counting up time since the last escape press 
		if (counter < .5)
		{
			counter += delta;
		}

		// Seeing if settings is open
		Settings node = (Settings)this.GetNode("Settings");



		// Sees if the user is trying to pause the game
		if (Input.IsActionPressed("ui_cancel") && (counter >= .5) && !node.Visible && (node.counter >= .3))
		{
			// Reset the timer
			counter = 0;

			//Switch visibility
			if (Visible)
			{
				GetTree().Paused = false;
				_close();
			}

			else
			{
				_open();
				GetTree().Paused = true;
			}

		}


	}
}

