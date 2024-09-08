using System;
using Godot;

public partial class Cutscene : Node
{
	// Music variables
	[Export] public MusicID MusicToPlay;
	[Export] public bool StopMusicAtEnd; // Music stops at the end of this cutscene

	// Scene file paths
	[Export(PropertyHint.File, "*.tscn")] public string NextScene;
	[Export(PropertyHint.File, "*.tscn")] public string CancelScene;

	// All the cutscene elements (need to implement ICutsceneElement)
	[Export] public Node[] ElementNodes;

	private ICutsceneElement[] _elements;
	private int _currentElement = 0;
	private bool _wasAnythingPressed = false;

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

		GlobalMusicPlayer.PlayMusic(MusicToPlay);
		StepAnimation();

		// Jump cut effect for loading screen
		LoadingScreen.TransTheFade = 0;
		AsciiFrog.NewVisRatio = 0;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_cancel") && LoadingScreen.TransTheFade <= 0)
			SkipScene();
		// If anything is just pressed down
		else if (Input.IsAnythingPressed() && !_wasAnythingPressed)
			StepAnimation();

		// Update anything pressed boolean
		_wasAnythingPressed = Input.IsAnythingPressed();
	}

	public async void StepAnimation()
	{
		// Base case, out of elements in array
		if (_currentElement >= _elements.Length)
		{
			// Wait until this frame has been rendered (lets the current animation finish)
			await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
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

		if (StopMusicAtEnd)
			GlobalMusicPlayer.StopMusic();
	}

	public void SkipScene()
	{
		GetTree().ChangeSceneToFile(CancelScene);

		GlobalMusicPlayer.StopMusic();
	}
}
