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
		{ToScene.Level1, 1}
	};
	private static readonly Dictionary<int, ToScene> _intToLevel = new()
	{
		{0, ToScene.PlayTestLevel},
		{1, ToScene.Level1}
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

		// Ensuring the file is done being changed
		saveFile.Close();
	}


	/*
		Method to get game data.
		Currently gets level data, but feel free to add on more.
	*/
	public static ToScene LoadData()
	{
		// Getting where we saved from
		FileAccess saveFile = FileAccess.Open(_saveTo, FileAccess.ModeFlags.Read);
		
		// Level to return
		ToScene giveLevel = ToScene.PlayTestLevel;

		// Json setup
		Json json = new Json();
		Error testFile = json.Parse(saveFile.GetLine());
		if (testFile == Error.Ok)
		{
			// Getting the value of the level from the save file
			Godot.Collections.Dictionary level = (Godot.Collections.Dictionary)json.Data;

			// Converting from dictionary to level
			giveLevel = _intToLevel[(int)level["PlayerAtLevel"]];
		}
		else
			GD.Print(testFile);

		// Ensuring the file is done being changed
		saveFile.Close();

		// Giving level 
		return giveLevel;

		

	}
}
