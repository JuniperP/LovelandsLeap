using Godot;

public partial class PauseScreen : Toggleable
{
	// Setup by ensuring the pause screen isn't visible and setting up key hold check
	public override void _Ready()
	{
		// Hide this scene till allowed
		Close();

		// Set up sfx
		SoundManager.ApplyButtonSFX(this);
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
		// Stopping the platforming theme music
		GetNode<MainPlatformingTheme>("/root/MainPlatformingThemeStream").Stop();
		SceneManager.SetNextGoTo(ToScene.MainMenu);
		LoadingScreen.FadeIn();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Seeing if settings is open
		Settings node = GetNode<Settings>("Settings");

		// Sees if the user is trying to pause the game
		if (Input.IsActionJustPressed("ui_cancel") && !node.Visible)
		{
			// Switch visibility
			if (Visible && LoadingScreen.TransTheFade <= 0)
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
	}
}
