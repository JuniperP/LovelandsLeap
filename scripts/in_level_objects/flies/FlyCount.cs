using Godot;

public partial class FlyCount : Node
{
	// Amount of flies gathered in the current level
	public static int FliesGottenLevel = 0;

	// Total flies gathered by player as of this level
	public static int FliesGottenLevelTotal = 0;

	// The saved amount of flies the player has gotten total
	public static int FliesGottenTotal = 0;

	// Dictionary of the number of flies per level
	public static int TotalLevelFlies = 0;

	// Total flies number in whole game
	public static int TotalGameFlies = 28;


	// Used to adjust around new games and loading games
	public static void NewFlyCount(int newCount)
	{
		FliesGottenLevelTotal = newCount;
	}

	public static void UpCount()
	{
		FliesGottenLevelTotal++;
		FliesGottenLevel++;
	}

}
