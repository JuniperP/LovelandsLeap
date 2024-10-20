using Godot;

public partial class Credits : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Jump cut effect
		LoadingScreen.TransTheFade = 0;
		AsciiFrog.NewVisRatio = 0;

		GlobalMusicPlayer.PlayMusic(GlobalMusicPlayer.GetSceneMusicID(ToScene.Credits));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Letting the user quit from the credits
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			SceneManager.SetNextGoTo(ToScene.MainMenu);
			SceneManager.GoToSetScene(this);
		}
	}
}
