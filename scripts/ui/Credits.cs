using Godot;

public partial class Credits : Control
{

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Letting the user quit from the credits
		if (Input.IsActionPressed("ui_cancel"))
		{
			GetTree().ChangeSceneToFile(SceneManager.GetPath(ToScene.MainMenu));
		}
	}
}
