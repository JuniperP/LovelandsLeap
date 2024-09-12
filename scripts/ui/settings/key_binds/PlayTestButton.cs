using Godot;
public partial class PlayTestButton : Button
{
	// Telling the node that its time to play test
	private static void PlayTest()
	{
		// Adjusting flies
		FlyCount.FliesGottenLevel = 0;
		FlyCount.TotalLevelFlies = 0;
		LoadLevelData.LoadData();
		FlyCount.FliesGottenLevelTotal = FlyCount.FliesGottenTotal;


		GlobalMusicPlayer.ToPlay = GlobalMusicPlayer.GetSceneMusicID(ToScene.PlayTestLevel);
		SceneManager.SetNextGoTo(ToScene.PlayTestLevel);
		LoadingScreen.FadeIn();
	}
}
