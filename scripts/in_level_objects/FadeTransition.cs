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

			// Saves the game
			LoadLevelData.SaveData(_sendsTo);



			// Playing the leaving sfx
			_leavingSFX.Play();

			if (_toEndCutscene)
			{
				// The run has finished!
				SpeedRunTimer.FinishedRun();

				// Giving the user the right ending
				if (FlyCount.FliesGottenTotal == 0)
				{
					SceneManager.SetNextGoTo(ToScene.HungryEnding);
					GlobalMusicPlayer.ToPlay = GlobalMusicPlayer.GetSceneMusicID(ToScene.HungryEnding);
				}
				else if (FlyCount.FliesGottenTotal == FlyCount.TotalGameFlies)
				{
					SceneManager.SetNextGoTo(ToScene.FullEnding);
					GlobalMusicPlayer.ToPlay = GlobalMusicPlayer.GetSceneMusicID(ToScene.FullEnding);
				}
				else
				{
					SceneManager.SetNextGoTo(ToScene.NormalEnding);
					GlobalMusicPlayer.ToPlay = GlobalMusicPlayer.GetSceneMusicID(ToScene.NormalEnding);
				}

			}

			// Sets up to go to the next area
			else
			{
				SceneManager.SetNextGoTo(_sendsTo);
				GlobalMusicPlayer.ToPlay = GlobalMusicPlayer.GetSceneMusicID(_sendsTo);
			}


			// Starts into next scene
			LoadingScreen.FadeIn();


		}
	}
}
