using Godot;

public partial class DialogueBox : Toggleable
{
	// Represents various states of the dialogue box
	private enum State
	{
		Inactive,
		Loading,
		Paused,
		Unloading
	}

	private State _loadState = State.Inactive;
	private Tween _tween;

	public override void _Ready()
	{
		// Set initial properties
		Hide();
		Modulate = Colors.Transparent;
	}

	// Step to the next dialogue state
	public void Step()
	{
		switch (_loadState)
		{
			case State.Inactive:
				Load();
				break;
			case State.Loading:
				Complete();
				break;
			case State.Paused:
				Unload();
				break;
			case State.Unloading:
				Deactivate();
				break;
		}
	}

	// Fade in box and start loading in dialogue
	private void Load()
	{
		Show();

		// Use a new tween for fading and callback
		_tween = CreateTween();
		_tween.TweenProperty(this, "modulate", Colors.White, 0.25).SetTrans(
			Tween.TransitionType.Sine
		);
		_tween.TweenCallback(Callable.From(Complete));

		_loadState = State.Loading;
	}

	// Complete fade and text
	private void Complete()
	{
		Modulate = Colors.White;

		_tween.Kill();

		_loadState = State.Paused;
	}

	// Fade out box
	private void Unload()
	{
		// Use a new tween for fading and callback
		_tween = CreateTween();
		_tween.TweenProperty(this, "modulate", Colors.Transparent, 0.25).SetTrans(
			Tween.TransitionType.Sine
		);
		_tween.TweenCallback(Callable.From(Deactivate));

		_loadState = State.Unloading;
	}

	// Completely hide and perform cutscene callback
	private void Deactivate()
	{
		Hide();
		Modulate = Colors.Transparent;

		_tween.Kill();

		_loadState = State.Inactive;
	}
}
