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
		GlobalMusicPlayer.PlayMusic(MusicID.MainMenu);
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
			SceneManager.SetNextGoTo(LoadLevelData.LoadData());

			// Queueing up the appropriate music
			int songID = (int)SceneManager.GetNextGoTo();

			if(songID == 2 || songID == 3)
				GlobalMusicPlayer.ToPlay = MusicID.Sunset;
			else if (3 < songID && songID < 7)
				GlobalMusicPlayer.ToPlay = MusicID.Forest;
			else if (6 <songID && songID < 10)
				GlobalMusicPlayer.ToPlay = MusicID.DeepWoods;
			else if (songID == 11)
				GlobalMusicPlayer.ToPlay = MusicID.TheOldestTree;
				
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
