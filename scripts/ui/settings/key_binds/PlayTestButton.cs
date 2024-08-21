using Godot;
public partial class PlayTestButton : Button
{
	// Telling the node that its time to play test
	private static void PlayTest()
	{
		LoadingScreen.NeedsToStartPlatTheme = true;
		SceneManager.SetNextGoTo(ToScene.PlayTestLevel);
		LoadingScreen.FadeIn();
	}
}
