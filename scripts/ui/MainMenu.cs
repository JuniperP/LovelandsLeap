using Godot;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
		// Set up button sfx
		SoundManager.ApplyButtonSFX(this);
	}

	// Sets up to start the game
	private void OnStartGameButtonPressed()
	{
		LoadLevelData.SaveData(ToScene.Level1);
		SceneManager.SetNextGoTo(ToScene.IntroCutscene);
	}

	// Sets up to run the credits
	private void OnCreditButtonPressed()
	{
		SceneManager.SetNextGoTo(ToScene.Credits);
	}

	// Quits the game from the menu after saving
	private void QuitGame()
	{
		LoadSettingsData.SaveData(false);
		GetTree().Quit();
	}
}
