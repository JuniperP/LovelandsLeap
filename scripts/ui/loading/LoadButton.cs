using Godot;

public partial class LoadButton : Button
{
	// Loading in a new game by fading screen and changing scene
	private void LoadSavedGame()
	{
		if (LoadLevelData.SavePathExist())
		{
			SceneManager.SetNextGoTo(LoadLevelData.LoadData());
			LoadingScreen.FadeIn();
		}
	}
}
