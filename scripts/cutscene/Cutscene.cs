using System;
using Godot;

public partial class Cutscene : Node
{
	[Export(PropertyHint.File, "*.tscn")] public string NextScene;
	[Export(PropertyHint.File, "*.tscn")] public string CancelScene;

	[Export] public Node[] ElementNodes;

	private ICutsceneElement[] _elements;
	private int _currentElement = 0;

	public override void _Ready()
	{
		// If nodes array is null then create empty array for elements
		if (ElementNodes is null)
			_elements = Array.Empty<ICutsceneElement>();
		// Otherwise, copy the nodes as cutscene elements
		else
		{
			_elements = new ICutsceneElement[ElementNodes.Length];
			try
			{
				// Copy all elements of the node array into _elements as ICutsceneElements
				ElementNodes.CopyTo(_elements, 0);
			}
			catch (InvalidCastException e)
			{
				throw new InvalidOperationException(
					"Invalid ElementNodes array. Ensure all nodes implement ICutsceneElement", e
				);
			}
		}

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
		if (_currentElement >= _elements.Length)
		{
			ChangeToNext();
			return;
		}

		// Perform an animation step on the current element
		_elements[_currentElement].Step(NextAnimation);
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
