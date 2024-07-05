using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class LoadSettingsData : Node
{
	// The file we will store to
	private static String StoreTo = "user://LoveLandSettingsInfo.cfg";


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
		config.SetValue("KeyBinds", "LeftIn", Keybinds._acts[UserAction.Left].Input);
		config.SetValue("KeyBinds", "RightIn",Keybinds._acts[UserAction.Right].Input);
		config.SetValue("KeyBinds", "JumpIn", Keybinds._acts[UserAction.Jump].Input);
		config.SetValue("KeyBinds", "DownIn", Keybinds._acts[UserAction.Down].Input);
		config.SetValue("KeyBinds", "ClickIn", Keybinds._acts[UserAction.Click].Input);
		config.SetValue("KeyBinds", "CancelIn", Keybinds._acts[UserAction.Cancel].Input);

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
		KeyBindSetterHelper.SetKeyBind((InputEvent) config.GetValue("KeyBinds", "LeftIn"), UserAction.Left);
		KeyBindSetterHelper.SetKeyBind((InputEvent) config.GetValue("KeyBinds", "RightIn"), UserAction.Right);
		KeyBindSetterHelper.SetKeyBind((InputEvent) config.GetValue("KeyBinds", "JumpIn"), UserAction.Jump);
		KeyBindSetterHelper.SetKeyBind((InputEvent) config.GetValue("KeyBinds", "DownIn"), UserAction.Down);
		KeyBindSetterHelper.SetKeyBind((InputEvent) config.GetValue("KeyBinds", "ClickIn"), UserAction.Click);
		KeyBindSetterHelper.SetKeyBind((InputEvent) config.GetValue("KeyBinds", "CancelIn"), UserAction.Cancel);
	}
}