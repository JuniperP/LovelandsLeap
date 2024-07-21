using Godot;

public partial class LoadButton : Button
{
	private void LoadSavedGame()
	{
		if (LoadLevelData.DoesSavePathExist())
		{
			SceneManager.SetNextGoTo(LoadLevelData.LoadData());
			LoadingScreen.FadeIn();
		}

	}
}
