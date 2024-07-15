using Godot;

public partial class MainMenu : Control
{
	// Where we will doing a scene change to
	private string _goTo;

	public override void _Ready()
	{
		// Set up button sfx
		SoundManager.ApplyButtonSFX(this);

		// Ensure scene isn't changed without permission
		_goTo = "";
	}

	// Sets up to start the game
	private void OnStartGameButtonPressed()
	{
		_goTo = SceneManager.GetPath(ToScene.IntroCutscene);
	}

	// Sets up to run the credits
	private void OnCreditButtonPressed()
	{
		_goTo = SceneManager.GetPath(ToScene.Credits);
	}

	// Quits the game from the menu after saving
	private void QuitGame()
	{
		LoadSettingsData.SaveData(false);
		GetTree().Quit();
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		// If asked, the scene switches
		if (!_goTo.Equals(""))
			GetTree().ChangeSceneToFile(_goTo);
	}
}
