using Godot;
public partial class PlayTestButton : Button
{
	// Telling the node that its time to play test
	private static void PlayTest()
	{
		GlobalMusicPlayer.ToPlay = GlobalMusicPlayer.GetSceneMusicID(ToScene.PlayTestLevel);
		SceneManager.SetNextGoTo(ToScene.PlayTestLevel);
		LoadingScreen.FadeIn();
	}
}
