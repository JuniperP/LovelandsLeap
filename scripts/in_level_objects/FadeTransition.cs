using Godot;

public partial class FadeTransition : Area2D
{
	// Where the player is going to
	[Export] private ToScene _sendsTo;
	[Export] private AudioStreamPlayer _leavingSFX;
	[Export] private bool _toEndCutscene = false;

	// What is signaled when the player enters - sends to new scene
	private void FrogEntered(Node2D node)
	{
		if (node is Player)
		{
			// Pauses so the player cant run around
			GetTree().Paused = true;

			// Saves the game
			LoadLevelData.SaveData(_sendsTo);

			// Resetting the amount flies for the in level count
			FlyCount.FliesGottenLevel = 0;
			FlyCount.TotalLevelFlies = 0;

			// Playing the leaving sfx
			_leavingSFX.Play();

			// Sets up to go to the next area
			SceneManager.SetNextGoTo(_sendsTo);

			// Queueing up the main platform theme for level 1
			if(_sendsTo == ToScene.Level1)
				LoadingScreen.NeedsToStartPlatTheme = true;

			// The run has finished!
			if(_toEndCutscene)
				SpeedRunTimer.FinishedRun();

			// Starts into next scene
			LoadingScreen.FadeIn();
		}
	}
}
