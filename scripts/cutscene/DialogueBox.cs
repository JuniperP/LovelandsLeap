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

	// [ExportGroup("Talking SFX")]
	[Export] public AudioStream TalkingAudio;

	[ExportGroup("Sprite Animation")]
	[Export] public AnimatedSprite2D Sprite;
	[Export] public string DefaultAnimation = "idle";
	[Export] public string TalkingAnimation = "talking";

	private State _loadState = State.Inactive;
	private Label _label;
	private Tween _tween;
	private Action _callBack;
	private AudioStreamPlayer _audioPlayer;

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

		// Load in dialogue box and start talking animation and sfx
		_tween.TweenProperty(this, "modulate", Colors.White, 0.25).SetTrans(
			Tween.TransitionType.Sine
		);
		TweenPlayAnimation(TalkingAnimation);
		_tween.TweenCallback(Callable.From(PlayAudio));

		// Load in text
		double textTime = _label.Text.Length / TextSpeed;
		_tween.TweenProperty(_label, "visible_ratio", 1, textTime);

		// Finish tween by calling complete method
		_tween.TweenCallback(Callable.From(Complete));

		_loadState = State.Loading;
	}

	// Complete fade and text
	private void Complete()
	{
		// Set sprite to talking and play audio
		PlayAnimation(TalkingAnimation);
		PlayAudio();

		// Set properties to complete values
		Modulate = Colors.White;
		_label.VisibleRatio = 1;

		_tween.Kill();

		_loadState = State.Paused;
	}

	// Fade out box
	private void Unload()
	{
		// Play default animation and stop audio
		PlayAnimation(DefaultAnimation);
		if (_audioPlayer.IsValid())
			_audioPlayer.QueueFree();

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

	private void PlayAnimation(string animation)
	{
		// If valid sprite and animation isn't the same
		if (Sprite.IsValid() && !Sprite.Animation.Equals(animation))
			Sprite.Play(animation);
	}

	private void TweenPlayAnimation(string animation)
	{
		if (Sprite.IsValid())
			_tween.TweenCallback(Callable.From(() => PlayAnimation(animation)));
	}

	private void PlayAudio()
	{
		// Return if the stream does not exist
		if (!TalkingAudio.IsValid())
			return;
		// Return if the player is already playing
		else if (_audioPlayer.IsValid() && !_audioPlayer.StreamPaused)
			return;

		// Create audio player and free it when it finishes playing
		_audioPlayer = new()
		{
			Bus = "Sound Effects",
			Stream = TalkingAudio
		};
		_audioPlayer.Finished += _audioPlayer.QueueFree;

		// Add to scene and start playing talking audio
		AddChild(_audioPlayer);
		_audioPlayer.Play();
	}
}
