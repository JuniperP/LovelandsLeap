using Godot;

public partial class FadeTransition : Area2D
{
	// Where the player is going to
	[Export] private ToScene _sendsTo;
	[Export] private AudioStreamPlayer _leavingSFX;

	// What is signaled when the player enters - sends to new scene
	private void FrogEntered(Node2D node)
	{
		if (node is Player)
		{
			// Pauses so the player cant run around
			GetTree().Paused = true;

			// Saves the game
			LoadLevelData.SaveData(_sendsTo);

			// Resting the amount flies for the in level count
			FlyCount.FliesGottenLevel = 0;
			FlyCount.TotalLevelFlies = 0;

			// Playing the leaving sfx
			_leavingSFX.Play();

			// Sets up to go to the next area
			SceneManager.SetNextGoTo(_sendsTo);

			// Starts into next scene
			LoadingScreen.FadeIn();
		}
	}
}
