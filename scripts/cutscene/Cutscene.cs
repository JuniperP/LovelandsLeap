using System;
using Godot;

public partial class Cutscene : Node
{
	[Export(PropertyHint.File, "*.tscn")] public string NextScene;
	[Export(PropertyHint.File, "*.tscn")] public string CancelScene;

	[Export] public Node[] Elements;

	private int _currentElement = 0;

	public override void _Ready()
	{
		// Create empty array if array is null
		Elements ??= Array.Empty<Node>();

		// Verify all cutscene elements are valid
		foreach (Node element in Elements)
			if (!element.IsCutsceneElement())
				throw new InvalidOperationException("Invalid custcene elements within Elements.");

		// CancelScene defaults to NextScene if value is null
		CancelScene ??= NextScene;

		StepAnimation();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_cancel") && LoadingScreen.TransTheFade <= 0)
			SkipScene();
		else if (Input.IsActionJustPressed("ui_accept"))
			StepAnimation();
	}

	public void StepAnimation()
	{
		// Base case, out of elements in array
		if (_currentElement >= Elements.Length)
		{
			ChangeToNext();
			return;
		}

		Node element = Elements[_currentElement];
		if (element is DialogueBox box)
			box.Step(NextAnimation);
		else if (element is AnimationPlayer anim)
			; // TODO: Implement animation playing
	}

	public void NextAnimation()
	{
		_currentElement++;
		StepAnimation();
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
