using Godot;

public partial class DialogueBox : Toggleable
{
	private enum State
	{
		Inactive,
		Loading,
		Paused
	}

	private State _loadState = State.Inactive;

	private void Step()
	{
		if (_loadState == State.Inactive)
			Load();
		else if (_loadState == State.Loading)
			Complete();
		else if (_loadState == State.Paused)
			Unload();
	}

	private void Load()
	{
		// Fade in box and start loading in dialogue
		_loadState = State.Loading;
	}

	private void Complete()
	{
		// Complete fade and text
		_loadState = State.Paused;
	}

	private void Unload()
	{
		// Fade out and perform callback
		_loadState = State.Inactive;
	}
}
