using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class LoadSettingsData : Node
{
	// The file we will store to
	private static String StoreTo = "res://ourUsersSettings.cfg";


	// Save settings data from the game
	public static void SaveData()
	{
		// User config file we will be utilizing access config data
		ConfigFile config = new ConfigFile();

		// Setting up sound data to be stored
		config.SetValue("Audio", "Master", AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master")));
		config.SetValue("Audio", "Music", AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Music")));
		config.SetValue("Audio", "SFX", AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Sound Effects")));


		// Setting up key binds to be stored
		// Putting in current input
		config.SetValue("KeyBindsInput", "LeftIn", Keybinds.LeftIn);
		config.SetValue("KeyBindsInput", "RightIn", Keybinds.RightIn);
		config.SetValue("KeyBindsInput", "JumpIn", Keybinds.JumpIn);
		config.SetValue("KeyBindsInput", "DownIn", Keybinds.DownIn);
		config.SetValue("KeyBindsInput", "ClickIn", Keybinds.ClickIn);
		config.SetValue("KeyBindsInput", "CancelIn", Keybinds.CancelIn);

		// Storing data, overwriting past settings
		config.Save(StoreTo);
	}


	// Load settings data into the game
	public static void LoadData()
	{
		// User config file we will be utilizing access config data
		ConfigFile config = new ConfigFile();

		// Ensuring a save file exists and backing out if not
		Error err = config.Load(StoreTo);
		if (err != Error.Ok)
			return;

		// Setting sound data to what it once was
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), (float)config.GetValue("Audio", "Master"));
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), (float)config.GetValue("Audio", "Music"));
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Sound Effects"), (float)config.GetValue("Audio", "SFX"));

		// Setting key binds to users choice
	}
}