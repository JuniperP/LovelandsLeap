using Godot;

public partial class LoadSettingsData : Node
{
	// The file we will store to
	private static string _storeTo = "user://lovelandsettings.cfg";

	// File for resetting settings
	private static string _defaultSettings = "user://lovelanddefaultsettings.cfg";


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
		config.SetValue("KeyBinds", "ClickIn", Keybinds._acts[UserAction.Click].Input);
		config.SetValue("KeyBinds", "CancelIn", Keybinds._acts[UserAction.Cancel].Input);


		// Setting up toggle button info to be stored
		config.SetValue("Display", "FullScreen", ToggleFullScreen.full);
		config.SetValue("Extra", "ClassicVer", ToggleClassicVerburg.classic);
		config.SetValue("Extra", "SpeedrunActive", ToggleSpeedrun.haveTimer);
		config.SetValue("Extra", "SpeedrunPB", ToggleSpeedrun.pbTime);


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
		KeyBindSetterHelper.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "LeftIn"), UserAction.Left);
		KeyBindSetterHelper.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "RightIn"), UserAction.Right);
		KeyBindSetterHelper.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "JumpIn"), UserAction.Jump);
		KeyBindSetterHelper.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "DownIn"), UserAction.Down);
		KeyBindSetterHelper.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "ClickIn"), UserAction.Click);
		KeyBindSetterHelper.SetKeyBind((InputEvent)config.GetValue("KeyBinds", "CancelIn"), UserAction.Cancel);

		// Matching if the screen is windowed
		ToggleFullScreen.full = (bool)config.GetValue("Display", "FullScreen");
		// Adjusting for if the screen isn't windowed for user
		if ((bool)config.GetValue("Display", "FullScreen"))
			// Full screened
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		else
			// Windowed
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);


		// Setting extra settings
		ToggleClassicVerburg.classic = (bool)config.GetValue("Extra", "ClassicVer");
		ToggleSpeedrun.haveTimer = (bool)config.GetValue("Extra", "SpeedrunActive");
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