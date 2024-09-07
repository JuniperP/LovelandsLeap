using Godot;

public partial class FlyCount : Node
{
	// Amount of flies gathered in the current level
	public static int FliesGottenLevel = 0;

	// Total flies gathered by player
	public static int FliesGottenTotal = 0;

	// Dictionary of the number of flies per level
	public static int TotalLevelFlies = 0;

	// Total flies number in whole game
	public static int TotalGameFlies = 22;


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

}
