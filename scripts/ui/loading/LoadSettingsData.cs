using Godot;

public partial class LoadSettingsData : Node
{
	// The file we will store to
	private static string _storeTo = "user://lovelandsleapsettings.cfg";

	// File for resetting settings
	private static string _defaultSettings = "user://lovelandsleapdefaultsettings.cfg";

	// Save settings data from the game
	public static void SaveData(bool setUpDefault)
	{
		// User config file we will be utilizing access config data
		ConfigFile config = new ConfigFile();


		// Setting up sound data to be stored
		config.SetValue("Audio", "Master", AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master")));
		config.SetValue("Audio", "Music", AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Music")));
		config.SetValue("Audio", "SFX", AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Sound Effects")));


		// Setting up key binds to be stored
		config.SetValue("KeyBinds", "LeftIn", Keybinds._acts[UserAction.Left].Input);
		config.SetValue("KeyBinds", "RightIn", Keybinds._acts[UserAction.Right].Input);
		config.SetValue("KeyBinds", "JumpIn", Keybinds._acts[UserAction.Jump].Input);
		config.SetValue("KeyBinds", "DownIn", Keybinds._acts[UserAction.Down].Input);
		config.SetValue("KeyBinds", "PActIn", Keybinds._acts[UserAction.PAction].Input);
		config.SetValue("KeyBinds", "SActIn", Keybinds._acts[UserAction.SAction].Input);
		config.SetValue("KeyBinds", "CancelIn", Keybinds._acts[UserAction.Cancel].Input);


		// Display info to be stored
		config.SetValue("Display", "FullScreen", ToggleFullScreen.IsFull);
		config.SetValue("Display", "Vsync", ToggleVSync.UseVsync);
		config.SetValue("Display", "WhichScreen", MonitorStandIn.ChosenScreen);


		// Extra info to be stored
		config.SetValue("Extra", "ClassicVer", ToggleClassicVerburg.Classic);
		config.SetValue("Extra", "HaveReaction", ToggleReaction.HaveReaction);
		config.SetValue("Extra", "SpeedrunActive", ToggleSpeedrun.HasTimer);
		config.SetValue("Extra", "SpeedrunPB", ToggleSpeedrun.PBTime);


		// Storing data, overwriting past settings
		if (setUpDefault)
			config.Save(_defaultSettings);
		else
			config.Save(_storeTo);
	}


	// Load settings data into the game
	public static void LoadData(bool setUpDefault)
	{
		// User config file we will be utilizing access config data
		ConfigFile config = new ConfigFile();

		// Loading in the correct file into config
		if (setUpDefault)
			config.Load(_defaultSettings);
		else
		{
			// Ensuring a save file exists and backing out if not
			Error err = config.Load(_storeTo);
			if (err != Error.Ok)
				return;
		}


		// Setting sound data to what it once was
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), (float)config.GetValue("Audio", "Master"));
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), (float)config.GetValue("Audio", "Music"));
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Sound Effects"), (float)config.GetValue("Audio", "SFX"));

		// Setting key binds to users choice
		KeyBindManager.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "LeftIn"), UserAction.Left);
		KeyBindManager.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "RightIn"), UserAction.Right);
		KeyBindManager.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "JumpIn"), UserAction.Jump);
		KeyBindManager.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "DownIn"), UserAction.Down);
		KeyBindManager.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "PActIn"), UserAction.PAction);
		KeyBindManager.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "SActIn"), UserAction.SAction);
		KeyBindManager.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "CancelIn"), UserAction.Cancel);

		// Matching if the screen is windowed
		ToggleFullScreen.IsFull = (bool)config.GetValue("Display", "FullScreen");
		ToggleFullScreen.AdjustWindowLocation();

		ToggleVSync.UseVsync = (bool)config.GetValue("Display", "Vsync");
		ToggleVSync.AdjustVsync();


		// Giving the preferred screen to play on
		MonitorStandIn.ChosenScreen = (int)config.GetValue("Display", "WhichScreen");


		// Setting extra settings
		ToggleClassicVerburg.Classic = (bool)config.GetValue("Extra", "ClassicVer");
		ToggleReaction.HaveReaction = (bool)config.GetValue("Extra", "HaveReaction");
		ToggleSpeedrun.HasTimer = (bool)config.GetValue("Extra", "SpeedrunActive");
		ToggleSpeedrun.NewTime((float)config.GetValue("Extra", "SpeedrunPB"));
	}


	public static void SetUpDefault()
	{
		// User config file we will be utilizing access config data
		ConfigFile config = new ConfigFile();

		// Ensuring the file doesn't exist to create a default file
		Error err = config.Load(_defaultSettings);
		if (err != Error.Ok)
			SaveData(true);
	}
}
