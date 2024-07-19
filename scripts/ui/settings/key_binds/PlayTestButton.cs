using Godot;
public partial class PlayTestButton : Button
{
	// Telling the node that its time to play test
	private void PlayTest()
	{
		SceneManager.SetNextGoTo(ToScene.PlayTestLevel);
		LoadingScreen.FadeIn();
	}
}
