using Godot;

public partial class LoadLevelData : Node
{
	// Where we will be saving all non config data to
	private static string _saveTo = "user://lovelandsleapsavedata.json";

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
		string jsonString = Json.Stringify((Godot.Collections.Dictionary)new() { { "PlayerAtLevel", (int)level } });

		// Storing level data
		saveFile.StoreLine(jsonString);

		// Converting the player's fly count into json format
		jsonString = Json.Stringify((Godot.Collections.Dictionary)new() { { "FlyCount", FlyCount.FliesGottenTotal } });

		// Storing fly data
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

		// Level to return (PlayTestLevel assigned for debugging)
		ToScene giveLevel = ToScene.PlayTestLevel;

		// Json setup
		Json json = new Json();

		// Getting levels
		Error testFile = json.Parse(saveFile.GetLine());
		if (testFile == Error.Ok)
		{
			// Getting the value of the level from the save file
			Godot.Collections.Dictionary level = (Godot.Collections.Dictionary)json.Data;

			// Converting from dictionary to level
			giveLevel = (ToScene)(int)level["PlayerAtLevel"];
		}
		else
			GD.Print(testFile);


		// Getting flies
		testFile = json.Parse(saveFile.GetLine());
		if (testFile == Error.Ok)
		{
			// Getting the value of the level from the save file
			Godot.Collections.Dictionary flyCount = (Godot.Collections.Dictionary)json.Data;

			// Converting from dictionary to level
			FlyCount.FliesGottenTotal = (int)flyCount["FlyCount"];
		}
		else
			GD.Print(testFile);

		// Ensuring the file is done being changed
		saveFile.Close();

		// Giving level 
		return giveLevel;
	}

	// Easy way to get whether a save file exits
	public static bool SavePathExist()
	{
		return FileAccess.FileExists(_saveTo);
	}
}
