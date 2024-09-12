using Godot;

public partial class MainMenu : Control
{
	// Signal to pop up confirmation new game menu
	[Signal] public delegate void TriedStartGameEventHandler();

	public override void _Ready()
	{
		// Set up button sfx
		SoundManager.ApplyButtonSFX(this);

		// Playing own music so splash and credits don't cause issues
		GlobalMusicPlayer.PlayMusic(GlobalMusicPlayer.GetSceneMusicID(ToScene.MainMenu));
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
		GlobalMusicPlayer.ToPlay = GlobalMusicPlayer.GetSceneMusicID(ToScene.IntroCutscene);

		// Resetting the flies you've obtained
		FlyCount.FliesGottenTotal = 0;
		FlyCount.FliesGottenLevelTotal = 0;

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
			SceneManager.SetNextGoTo(LoadLevelData.LoadData());

			// Setting up your fly count
			FlyCount.FliesGottenLevelTotal = FlyCount.FliesGottenTotal;

			// Queueing up the appropriate music
			GlobalMusicPlayer.ToPlay = GlobalMusicPlayer.GetSceneMusicID(SceneManager.GetNextGoTo());

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
