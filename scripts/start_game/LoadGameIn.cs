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

		// Adjusting which screen is displayed to based on the users preferred monitor
		if (MonitorStandIn.chosenScreen >= DisplayServer.GetScreenCount())
			MonitorStandIn.tempChoice = GetWindow().CurrentScreen;
		else
			DisplayServer.WindowSetCurrentScreen(MonitorStandIn.chosenScreen, GetWindow().GetWindowId());

		// Starts the splash sequence
		EmitSignal(SignalName.StartSplash, true);

	}
}
