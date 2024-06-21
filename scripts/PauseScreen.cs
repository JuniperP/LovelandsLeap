using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class PauseScreen : Toggleable
{
	// Effectively the timer we use to ensure a reasonable amount of time passes between our events
	double counter;

	// Setup
	public override void _Ready()
	{
		Visible = false;
		counter = 0;
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
		//Counting up time since the last escape press 
		if (counter < .5)
		{
			counter += delta;
		}


		// Sees if the user is trying to pause the game
		if (Input.IsActionPressed("ui_cancel") && (counter>=.5))
		{
			// Reset the timer
			counter = 0;

			//Switch visibility
			if (Visible)
			{
				_close();
			}
			else
			{
				_open();
			}

		}


	}
}

