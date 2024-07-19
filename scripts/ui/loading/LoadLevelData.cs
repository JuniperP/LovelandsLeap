using Godot;
using System.Collections.Generic;

public partial class LoadLevelData : Node
{
	// Where we will be saving all non config data to
	private static string _saveTo = "user://lovelandsavedata.json";

	// Dictionaries of what stored values correspond to what
	private static readonly Dictionary<ToScene, int> _levelToInt = new()
	{
		{ToScene.PlayTestLevel, 0},
	};
	private static readonly Dictionary<int, ToScene> _intToLevel = new()
	{
		{0, ToScene.PlayTestLevel},
	};


	/*
		Method to store game data.
		Note 1: Currently just stores level data, but feel free to add on more.
		Note 2: Storing via a dictionary entry to make debugging easier.
	*/
	public static void SaveData(ToScene level)
	{
		// Getting where we will be saving to
		FileAccess saveFile = FileAccess.Open(_saveTo, FileAccess.ModeFlags.Write);

		// Converting where the player currently is into json format
		string jsonString = Json.Stringify((Godot.Collections.Dictionary)new() { { "PlayerAtLevel", _levelToInt[level] } });

		// Storing data
		saveFile.StoreLine(jsonString);
	}


	public static ToScene LoadData()
	{
		// Getting where we saved from
		FileAccess saveFile = FileAccess.Open(_saveTo, FileAccess.ModeFlags.Write);

		// Getting the value of the level from the save file
		Godot.Collections.Dictionary level = (Godot.Collections.Dictionary)Json.ParseString(saveFile.GetLine());

		// Converting from dictionary to int
		return _intToLevel[(int)level["PlayerAtLevel"]];

	}
}
