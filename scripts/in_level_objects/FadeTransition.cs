using Godot;

public partial class FadeTransition : Area2D
{
	// Where the player is going to
	[Export] private ToScene _sendsTo;
	[Export] private bool _toEndCutscene = false;
	[Export] private AudioStreamPlayer _leavingSFX;


	// What is signaled when the player enters - sends to new scene
	private void FrogEntered(Node2D node)
	{
		if (node is Player)
		{
			// Pauses so the player cant run around
			GetTree().Paused = true;

			// Resetting the amount flies for the in level count
			FlyCount.FliesGottenTotal = FlyCount.FliesGottenLevelTotal;
			FlyCount.FliesGottenLevel = 0;
			FlyCount.TotalLevelFlies = 0;

			// Playing the leaving sfx
			_leavingSFX.Play();


			// Set up for after this level
			if (_toEndCutscene)
			{
				// The run has finished!
				SpeedRunTimer.FinishedRun();

				// Giving the user the right ending
				if (FlyCount.FliesGottenTotal == 0)
					SetupNextArea(ToScene.HungryEnding);
				else if (FlyCount.FliesGottenTotal == FlyCount.TotalGameFlies)
					SetupNextArea(ToScene.FullEnding);
				else
					SetupNextArea(ToScene.NormalEnding);
			}

			else
				SetupNextArea(_sendsTo);


			// Starts into next scene
			LoadingScreen.FadeIn();


		}
	}


	private static void SetupNextArea(ToScene newArea)
	{
		// Saves the game for the level to go to
		LoadLevelData.SaveData(newArea);

		// Sets the area to go to loading wise 
		SceneManager.SetNextGoTo(newArea);

		// Configures the new music
		GlobalMusicPlayer.ToPlay = GlobalMusicPlayer.GetSceneMusicID(newArea);
	}
}
