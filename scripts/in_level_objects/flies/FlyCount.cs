using System.Collections.Generic;
using Godot;

public partial class FlyCount : Node
{
	// Amount of flies gathered in the current level
	public static int FliesGottenLevel = 0;

	// Total flies gathered by player
	public static int FliesGottenTotal = 0;

	// Dictionary of the number of flies per level
	public static readonly Dictionary<ToScene, int> _numFlies = new()
	{
		{ToScene.PlaceHolder, 0}, // Only activated in testing so no errors
		{ToScene.PlayTestLevel, 0},
		{ToScene.IntroCutscene, 3}, // 3 for testing, but needed because Juniper doesn't use SceneManger system
		{ToScene.Tutorial, 0},
		{ToScene.Level1, 3}
	};

	// Total flies number in whole game
	public static int TotalFlies = 20;


	// Used to adjust around new games and loading games
	public static void NewFlyCount(int newCount)
	{
		FliesGottenTotal = newCount;
	}

	public static void UpCount()
	{
		FliesGottenTotal++;
		FliesGottenLevel++;
	}
	
	public static void EnteringNewArea()
	{
		// Resting the amount
		FliesGottenLevel = 0;

		// TODO: Saving the number of flies gotten
	}

}
