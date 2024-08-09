using System;
using Godot;

public partial class DialogueBox : Toggleable, ICutsceneElement
{
	// Represents various states of the dialogue box
	private enum State
	{
		Inactive,
		Loading,
		Paused,
		Unloading
	}

	// Speed for loading text in characters per second
	[Export] public float TextSpeed = 40f;

	[ExportGroup("Sprite Animation")]
	[Export] public AnimatedSprite2D Sprite;
	[Export] public string DefaultAnimation = "idle";
	[Export] public string TalkingAnimation = "talking";

	private State _loadState = State.Inactive;
	private Label _label;
	private Tween _tween;
	private Action _callBack;

	public override void _Ready()
	{
		_label = GetNode<Label>("BackBox/Text");

		// Set initial properties
		if (Sprite.IsValid())
			Sprite.Play(DefaultAnimation);
		Hide();
		Modulate = Colors.Transparent;
		_label.VisibleRatio = 0;
	}

	// Step to the next dialogue state, calls callback when deactivated
	public void Step(Action deactivationCallback)
	{
		_callBack = deactivationCallback;

		// Step to next state based on current state
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
		TweenPlayAnimation(TalkingAnimation);

		double textTime = _label.Text.Length / TextSpeed;
		_tween.TweenProperty(_label, "visible_ratio", 1, textTime);

		_tween.TweenCallback(Callable.From(Complete));

		_loadState = State.Loading;
	}

	private void TweenPlayAnimation(string animation)
	{
		if (Sprite.IsValid())
			_tween.TweenCallback(Callable.From(() => Sprite.Play(animation)));
	}

	// Complete fade and text
	private void Complete()
	{
		// Set properties to complete values
		Modulate = Colors.White;
		_label.VisibleRatio = 1;

		_tween.Kill();

		_loadState = State.Paused;
	}

	// Fade out box
	private void Unload()
	{
		if (Sprite.IsValid())
			Sprite.Play(DefaultAnimation);

		// Use a new tween for fading and callback
		_tween = CreateTween();
		_tween.TweenProperty(this, "modulate", Colors.Transparent, 0.25).SetTrans(
			Tween.TransitionType.Sine
		);
		_tween.TweenInterval(1); // 1 second delay before deactivation
		_tween.TweenCallback(Callable.From(Deactivate));

		_loadState = State.Unloading;
	}

	// Completely hide and perform cutscene callback
	private void Deactivate()
	{
		// Set properties to hidden values
		Hide();
		Modulate = Colors.Transparent;
		_label.VisibleRatio = 0;

		_tween.Kill();

		_loadState = State.Inactive;

		// Perform callback
		_callBack();
	}
}
