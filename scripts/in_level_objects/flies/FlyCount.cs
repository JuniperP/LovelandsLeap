using Godot;

public partial class FlyCount : Node
{
	// Total flies gathered by player
	public static int FliesGotten = 0;

	// Total flies in whole game
	private static int _totalFlies = 45; // (current number is a rough guess)

	// Used to adjust around new games and loading games
	public static void NewFlyCount(int newCount)
	{
		FliesGotten = newCount;
	}

	public static void UpCount()
	{
		FliesGotten++;
	}

	public static int GetCollectedRatio()
	{
		// Ratio as a decimal
		float percent = FliesGotten / _totalFlies;

		// Ratio as a rounded number
		return (int)(percent * 100);
	}

}