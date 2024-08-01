using Godot;

public partial class Cutscene : Node
{
	[Export(PropertyHint.File, "*.tscn")] public string NextScene;
	[Export(PropertyHint.File, "*.tscn")] public string CancelScene;

	public override void _Ready()
	{
		// CancelScene defaults to NextScene if value is null
		CancelScene ??= NextScene;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_cancel") && LoadingScreen.TransTheFade <= 0)
			SkipScene();
	}

	public void ChangeToNext()
	{
		GetTree().ChangeSceneToFile(NextScene);
	}

	public void SkipScene()
	{
		GetTree().ChangeSceneToFile(CancelScene);
	}
}
