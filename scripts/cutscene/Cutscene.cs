using Godot;

public partial class Cutscene : Node
{
	[Export] public PackedScene NextScene;
	[Export] public PackedScene CancelScene;

	public override void _Ready()
	{
		// CancelScene defaults to NextScene if value is null
		CancelScene ??= NextScene;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_cancel"))
			SkipScene();
    }

	public void ChangeToNext()
	{
		GetTree().ChangeSceneToPacked(NextScene);
	}

	public void SkipScene()
	{
		GetTree().ChangeSceneToPacked(CancelScene);
	}
}
