using Godot;


public partial class LoadGameIn : Control
{
	// Signal to start the splash sequence
	[Signal] public delegate void StartSplashEventHandler(InputEvent input);


	// Sets up the game
	public override void _Ready()
	{
		// Set up for easy default settings
		LoadSettingsData.SetUpDefault();

		// Loads in user settings
		LoadSettingsData.LoadData(false);

		//TEST
		LoadLevelData.SaveData(ToScene.Credits);

		// Starts the splash sequence
		EmitSignal(SignalName.StartSplash, true);

	}

	// Used to finish loading when the game is ready
	private void LogoIn()
	{
		// Send to main screen
		SceneManager.SetNextGoTo(LoadLevelData.LoadData());
		//SceneManager.SetNextGoTo(ToScene.MainMenu);
		SceneManager.GoToSetScene(this);
	}
}
