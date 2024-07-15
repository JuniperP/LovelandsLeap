using Godot;

public partial class PauseScreen : Toggleable
{
	// Boolean to see if a cancel is held down
	private bool _heldDown;

	// Setup by ensuring the pause screen isn't visible and setting up key hold check
	public override void _Ready()
	{
		// Hide this scene till allowed
		Close();

		// Set up sfx
		SoundManager.ApplyButtonSFX(this);

		// Initially set to true in the case where escape is held entering a scene
		_heldDown = true;
	}

	// Overriding the close method to also unpause the game
	protected override void Close()
	{
		GetTree().Paused = false;
		Visible = false;
	}

	// Return to the main menu
	private void ToMainMenu()
	{
		SceneManager.SetNextGoTo(ToScene.MainMenu);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		// Seeing if settings is open
		Settings node = GetNode<Settings>("Settings");


		// Sees if the user is trying to pause the game
		if (Input.IsActionPressed("ui_cancel") && (!_heldDown) && !node.Visible)
		{
			// Cancel must be currently held down and deemed as such
			_heldDown = true;

			//Switch visibility
			if (Visible)
			{
				GetTree().Paused = false;
				Close();
			}

			else
			{
				Open();
				GetTree().Paused = true;
			}

		}

		// Key is deemed as not held down if not pressed when pause screen stands alone
		else if (!Input.IsActionPressed("ui_cancel") && _heldDown && !node.Visible)
			_heldDown = false;

		// Assumes the user will be holding down the escape key when exiting the settings menu
		else if (node.Visible)
			_heldDown = true;

	}
}

