using Godot;
using System;

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

		//Putting in current labels
		config.SetValue("KeyBindsSymbol", "LeftSym", Keybinds.LeftSym);
		config.SetValue("KeyBindsSymbol", "RightSym", Keybinds.RightSym);
		config.SetValue("KeyBindsSymbol", "JumpSym", Keybinds.JumpSym);
		config.SetValue("KeyBindsSymbol", "DownSym", Keybinds.DownSym);
		config.SetValue("KeyBindsSymbol", "ClickSym", Keybinds.ClickSym);
		config.SetValue("KeyBindsSymbol", "CancelSym", Keybinds.CancelSym);

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



// Failed load settings code:

// 	// Where our data is saving to
// 	private static String path = "user://LoveLandSave.save";


// 	// Saving our data
// 	public static void Save()
// 	{
// 		// Getting file access to a position we will write to
// 		FileAccess ToFile = FileAccess.Open(path, FileAccess.ModeFlags.Write);


// 		// Storing aspects of settings via types in this file:
// 		// Getting the events to be restored as actions for the input map
// 		ToFile.StoreVar(Keybinds.LeftIn);
// 		ToFile.StoreVar(Keybinds.RightIn);
// 		ToFile.StoreVar(Keybinds.JumpIn);
// 		ToFile.StoreVar(Keybinds.ClickIn);


// 		// Getting the set volumes of all of the busses
// 		ToFile.StoreFloat(Mathf.DbToLinear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master"))));
// 		ToFile.StoreFloat(Mathf.DbToLinear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Music"))));
// 		ToFile.StoreFloat(Mathf.DbToLinear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Sound Effects"))));

// 	}



// 	public static void LoadData()
// 	{
// 		// Making sure our file is gotten
// 		if (FileAccess.FileExists(path))
// 		{
// 			// Getting file access to a position we will read from
// 			FileAccess FromFile = FileAccess.Open(path, FileAccess.ModeFlags.Read);


// 			// Setting up past data
// 			// Events in actions
// 			Keybinds.LeftIn = (InputEvent)FromFile.GetVar(true);
// 			Keybinds.RightIn = (InputEvent)FromFile.GetVar(true);
// 			Keybinds.JumpIn = (InputEvent)FromFile.GetVar(true);
// 			Keybinds.ClickIn = (InputEvent)FromFile.GetVar(true);

// 			// Setup off of gotten events
// 			InputMap.ActionEraseEvents("move_left");
// 			Keybinds.LeftSym = Keybinds.LeftIn.AsText();
// 			InputMap.ActionAddEvent("move_left", Keybinds.LeftIn);
// 			InputMap.ActionEraseEvents("move_right");
// 			Keybinds.RightSym = Keybinds.RightIn.AsText();
// 			InputMap.ActionAddEvent("move_right", Keybinds.RightIn);
// 			InputMap.ActionEraseEvents("move_up");
// 			Keybinds.JumpSym = Keybinds.JumpIn.AsText();
// 			InputMap.ActionAddEvent("move_up", Keybinds.JumpIn);
// 			InputMap.ActionEraseEvents("primary_click");
// 			Keybinds.ClickSym = Keybinds.ClickIn.AsText();
// 			InputMap.ActionAddEvent("primary_click", Keybinds.ClickIn);


// 			// Audio info
// 			float Master = FromFile.GetFloat();
// 			float Music = FromFile.GetFloat();
// 			float SFX = FromFile.GetFloat();

// 			// Setup off audio
// 			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), Mathf.LinearToDb(Master));
// 			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), Mathf.LinearToDb(Music));
// 			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Sound Effects"), Mathf.LinearToDb(SFX));


// 		}

// 		else
// 		{
// 			// Everything is already assigned a default value, no further info is required
// 		}
// 	}

