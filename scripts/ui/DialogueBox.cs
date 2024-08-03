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

	public override void _Ready()
	{

		Hide();
		Modulate = new Color(1, 1, 1, 0);
	}

	public void Step()
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
		Show();
		Modulate = new Color(1, 1, 1, 0.5f);

		_loadState = State.Loading;
	}

	private void Complete()
	{
		// Complete fade and text
		Show();
		Modulate = new Color(1, 1, 1, 1);

		_loadState = State.Paused;
	}

	private void Unload()
	{
		// Fade out and perform callback
		Hide();
		Modulate = new Color(1, 1, 1, 0);

		_loadState = State.Inactive;
	}
}
