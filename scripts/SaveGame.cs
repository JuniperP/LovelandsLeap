using Godot;
using System;


public partial class SaveGame : Node
{
	// Where our data is saving to
	private static String path = "res://save/UserSave.save";



	// Saving our data
	public static void Save()
	{
		// Getting file access to a position we will write to
		Godot.FileAccess ToFile = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Write);


		// Storing aspects of settings via types in this file:
		// Getting the events to be restored as actions for the input map
		ToFile.StoreVar(Keybinds.LeftIn);
		// ToFile.StoreVar(Keybinds.RightIn);
		// ToFile.StoreVar(Keybinds.JumpIn);
		// ToFile.StoreVar(Keybinds.ClickIn);
		

		// Getting the set volumes of all of the busses
		ToFile.StoreFloat(Mathf.DbToLinear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Master"))));
		ToFile.StoreFloat(Mathf.DbToLinear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Music"))));
		ToFile.StoreFloat(Mathf.DbToLinear(AudioServer.GetBusVolumeDb(AudioServer.GetBusIndex("Sound Effects"))));

	}



	public static void LoadData()
	{
		// Making sure our file is gotten
		if(FileAccess.FileExists(path))
		{
			// Getting file access to a position we will read from
			FileAccess FromFile = FileAccess.Open(path, FileAccess.ModeFlags.Read);


			// Setting up past data
			// Events in actions
			Keybinds.LeftIn = (InputEvent) FromFile.GetVar(true);
			// Keybinds.RightIn = (InputEvent) FromFile.GetVar(true);
			// Keybinds.JumpIn = (InputEvent) FromFile.GetVar(true);
			// Keybinds.ClickIn = (InputEvent) FromFile.GetVar(true);

			// // Setup off of gotten events
			InputMap.ActionEraseEvents("move_left");
			Keybinds.LeftSym = Keybinds.LeftIn.AsText();
			InputMap.ActionAddEvent("move_left", Keybinds.LeftIn);
			// InputMap.ActionEraseEvents("move_right");
			// Keybinds.RightSym = Keybinds.RightIn.AsText();
			// InputMap.ActionAddEvent("move_right", Keybinds.RightIn);
			// InputMap.ActionEraseEvents("move_up");
			// Keybinds.JumpSym = Keybinds.JumpIn.AsText();
			// InputMap.ActionAddEvent("move_up", Keybinds.JumpIn);
			// InputMap.ActionEraseEvents("primary_click");
			// Keybinds.ClickSym = Keybinds.ClickIn.AsText();
			// InputMap.ActionAddEvent("primary_click", Keybinds.ClickIn);


			// Audio info
			float Master = FromFile.GetFloat();
			float Music = FromFile.GetFloat();
			float SFX = FromFile.GetFloat();

			// Setup off audio
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), Mathf.LinearToDb(Master));
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), Mathf.LinearToDb(Music));
			AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Sound Effects"), Mathf.LinearToDb(SFX));
			

		}

		else
		{
			// Everything is already assigned a default value
		}
	}
}
