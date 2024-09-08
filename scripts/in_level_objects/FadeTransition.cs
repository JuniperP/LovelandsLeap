using Godot;

public partial class FadeTransition : Area2D
{
	// Where the player is going to
	[Export] private ToScene _sendsTo;
	[Export] private bool _toEndCutscene = false;
	[Export] private MusicID _nextAreasTheme = MusicID.DeepWoods;
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

			// Resetting the amount flies for the in level count
			FlyCount.FliesGottenLevel = 0;
			FlyCount.TotalLevelFlies = 0;

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
					GlobalMusicPlayer.ToPlay = MusicID.Cutscene;
				}	
				else if (FlyCount.FliesGottenTotal == FlyCount.TotalGameFlies)
				{
					SceneManager.SetNextGoTo(ToScene.FullEnding);
					//GlobalMusicPlayer.ToPlay = 
				}
				else
				{
					SceneManager.SetNextGoTo(ToScene.NormalEnding);
					GlobalMusicPlayer.ToPlay = MusicID.Cutscene;
				}
					
			}

			// Sets up to go to the next area
			else
			{
				SceneManager.SetNextGoTo(_sendsTo);
				GlobalMusicPlayer.ToPlay = _nextAreasTheme;
			}


			// Starts into next scene
			LoadingScreen.FadeIn();


		}
	}
}
