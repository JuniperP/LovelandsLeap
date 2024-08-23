using Godot;

public partial class MainMenu : Control
{
	// Signal to pop up confirmation new game menu
	[Signal] public delegate void TriedStartGameEventHandler();

	public override void _Ready()
	{
		// Set up button sfx
		SoundManager.ApplyButtonSFX(this);
	}

	// Sets up to start the game
	private void OnNewGameButtonPressed()
	{
		if (LoadLevelData.SavePathExist())
			EmitSignal(SignalName.TriedStartGame);
		else
			StartNewGame();
	}

	// Confirm starts a new game
	private void StartNewGame()
	{
		// Overwrite save data
		LoadLevelData.SaveData(ToScene.Tutorial);

		// Accounting for speedrun
		if (ToggleSpeedrun.HasTimer)
			SpeedRunTimer.StartSpeedrun();

		// Going to the start of game
		SceneManager.SetNextGoTo(ToScene.IntroCutscene);
		LoadingScreen.FadeIn();

	}

	// Loading in a new game by fading screen and changing scene
	private void LoadSavedGame()
	{
		if (LoadLevelData.SavePathExist())
		{
			LoadingScreen.NeedsToStartPlatTheme = true;
			SceneManager.SetNextGoTo(LoadLevelData.LoadData());
			LoadingScreen.FadeIn();
		}
	}

	// Sets up to run the credits
	private void OnCreditButtonPressed()
	{
		SceneManager.SetNextGoTo(ToScene.Credits);
	}

	// Quits the game from the menu after saving
	private void QuitGame()
	{
		LoadSettingsData.SaveData(false);
		GetTree().Quit();
	}
}
