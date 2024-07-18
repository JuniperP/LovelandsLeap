using Godot;

public partial class AsciiFrog : RichTextLabel
{
	// Stating whether we can fade or not
	private bool _canFade;

	// Which direction to fade
	private bool _fadeOut;

	// Static way of seeing the texts VisibleRatio
	public static float newVisRatio = 0;


	// Signal to say we have faded out
	[Signal] public delegate void FadedOutEventHandler();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		VisibleRatio = newVisRatio;

		if (VisibleRatio >= 1)
		{
			_canFade = true;
			_fadeOut = true;
		}
		else
		{
			_canFade = false;
			_fadeOut = false;
		}

	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// If allowed to fade, fades as needed
		if (_canFade)
		{
			if (_fadeOut)
			{
				delta *= -1f;

				if (VisibleRatio <= 0)
				{
					EmitSignal(SignalName.FadedOut);
					_canFade = false;
				}
			}

			else
			{
				if (VisibleRatio >= 1f)
				{
					_canFade = false;
					// Full loading animation is done so we switch scenes
					SceneManager.SetNextGoTo(LoadingScreen.nextScene);
					SceneManager.GoToSetScene(this);
				}
			}

			// Type out frog accordingly
			newVisRatio += (float)delta;
			VisibleRatio = newVisRatio;

		}
	}

	// Easy signal transfers to switch fading type
	private void FadeIn()
	{
		_fadeOut = false;
		_canFade = true;
	}

}
