using Godot;
using System.Collections.Generic;

public partial class LoadLevelData : Node
{
	// Where we will be saving all non config data to
	private static string _saveTo = "user://lovelandsavedata.json";

	// Dictionary of what stored values correspond to what
	private static readonly Dictionary<ToScene, int> _level = new()
	{
		{ToScene.PlayTestLevel, 0},
	};


	/*
		Method to store game data.
		Currently just stores level data, but feel free to add on more.
	*/
	public static void SaveData(ToScene level)
	{
		// Getting where we will be saving to
		FileAccess saveFile = FileAccess.Open(_saveTo, FileAccess.ModeFlags.Write);

		// Converting where the player currently is into json format
		string jsonString = Json.Stringify((Godot.Collections.Dictionary) new() { { "PlayerAtLevel", _level[level] } });

		// Storing data
		saveFile.StoreLine(jsonString);
	}
}
