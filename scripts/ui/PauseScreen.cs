using Godot;
using System;


public partial class PauseScreen : Toggleable
{
	// Boolean to see if a cancel is held down
	private Boolean HeldDown;


	// Setup by ensuring the pause screen isn't visible and setting up key hold check
	public override void _Ready()
	{
		_close();

		// Set up sfx
		SoundManager.ApplyButtonSFX(this);

		// Initially set to true in the case where escape is held entering a scene
		HeldDown = true;
	}

	// Overriding the close method to also unpause the game
	protected override void _close()
	{
		GetTree().Paused = false;
		Visible = false;
	}

	// Return to the main menu
	private void _to_main_menu()
	{
		_close();
		GetTree().ChangeSceneToFile("res://scenes/ui/main_menu.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		// Seeing if settings is open
		Settings node = (Settings)GetNode("Settings");


		// Sees if the user is trying to pause the game
		if (Input.IsActionPressed("ui_cancel") && (!HeldDown) && !node.Visible)
		{
			// Cancel must be currently held down and deemed as such
			HeldDown = true;

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

		// Key is deemed as not held down if not pressed when pause screen stands alone
		else if (!Input.IsActionPressed("ui_cancel") && (HeldDown) && !node.Visible)
		{
			HeldDown = false;
		}

		// Assumes the user will be holding down the escape key when exiting the settings menu
		else if (node.Visible)
		{
			HeldDown = true;
		}


	}
}

