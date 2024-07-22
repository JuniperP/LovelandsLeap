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
		if(LoadLevelData.SavePathExist())
			EmitSignal(SignalName.TriedStartGame);
		else
			StartNewGame();
	}

	// Confirm starts a new game
	private void StartNewGame()
	{
		LoadLevelData.SaveData(ToScene.Level1);
		SceneManager.SetNextGoTo(ToScene.IntroCutscene);
		LoadingScreen.FadeIn();

		// Accounting for speedrun
		if(ToggleSpeedrun.HaveTimer)
			SpeedRunTimer.StartSpeedrun();
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
