using Godot;
using System;

public partial class LoadButton : Button
{
	private void LoadSavedGame()
	{
		if(FileAccess.FileExists(LoadLevelData.saveTo))
		{
			SceneManager.SetNextGoTo(LoadLevelData.LoadData());
			LoadingScreen.FadeIn();
		}
		
	}
}
