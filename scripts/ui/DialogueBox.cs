using Godot;

public partial class DialogueBox : Toggleable
{
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
		Hide();
		Modulate = Colors.Transparent;
	}

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

	private void Load()
	{
		// Fade in box and start loading in dialogue
		Show();

		_tween = CreateTween();
		_tween.TweenProperty(this, "modulate", Colors.White, 0.25).SetTrans(
			Tween.TransitionType.Sine
		);
		_tween.TweenCallback(Callable.From(Complete));

		_loadState = State.Loading;
	}

	private void Complete()
	{
		// Complete fade and text
		Modulate = Colors.White;

		_tween.Kill();

		_loadState = State.Paused;
	}

	private void Unload()
	{
		// Fade out
		_tween = CreateTween();
		_tween.TweenProperty(this, "modulate", Colors.Transparent, 0.25).SetTrans(
			Tween.TransitionType.Sine
		);
		_tween.TweenCallback(Callable.From(Deactivate));

		_loadState = State.Unloading;
	}

	private void Deactivate()
	{
		// Completely hide and perform cutscene callback
		Hide();
		Modulate = Colors.Transparent;

		_tween.Kill();

		_loadState = State.Inactive;
	}
}
