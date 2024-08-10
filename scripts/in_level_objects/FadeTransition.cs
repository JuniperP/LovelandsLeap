using Godot;

public partial class FadeTransition : Area2D
{
	// Where the player is going to
	[Export] private ToScene _sendsTo;

	// What is signaled when the player enters - sends to new scene
	private void FrogEntered(Node2D node)
	{
		if (node is Player)
		{
			// Saves the game
			LoadLevelData.SaveData(_sendsTo);

			// Sets up to go to the next area
			SceneManager.SetNextGoTo(_sendsTo);

			// Pauses so the player cant run around
			GetTree().Paused = true;

			// Starts into next scene
			LoadingScreen.FadeIn();
		}
	}
}
