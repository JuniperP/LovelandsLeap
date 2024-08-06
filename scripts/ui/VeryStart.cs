using Godot;

public partial class VeryStart : Control
{

	// Goes to the splash screen seamlessly
	private void LoadGame()
	{
		// Epileptic proof pause
		SceneTreeTimer timer = GetTree().CreateTimer(.5);
		timer.Timeout += () =>
		{
			SceneManager.SetNextGoTo(ToScene.SplashScreen);
			SceneManager.GoToSetScene(this);
		};

	}

}
