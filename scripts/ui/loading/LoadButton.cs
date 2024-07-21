using Godot;

public partial class LoadButton : Button
{
	private void LoadSavedGame()
	{
		if (LoadLevelData.SavePathExist())
		{
			SceneManager.SetNextGoTo(LoadLevelData.LoadData());
			LoadingScreen.FadeIn();
		}

	}
}
