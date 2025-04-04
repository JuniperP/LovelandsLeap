using Godot;

public partial class AsciiFrog : RichTextLabel
{
	// Stating whether we can fade or not
	private bool _canFade;

	// Which direction to fade
	private bool _fadeOut;

	// Static way of seeing the texts VisibleRatio
	public static float NewVisRatio = 0;

	// Signal to say we have faded out
	[Signal] public delegate void FadedOutEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		VisibleRatio = NewVisRatio;

		if (VisibleRatio > 0)
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
			if (_fadeOut) // Fading out
			{
				// STARTING RIGHT MUSIC AS NEW AREA IS BEING ENTERED!
				GlobalMusicPlayer.PlayMusic(GlobalMusicPlayer.ToPlay);


				delta *= -1f;

				if (VisibleRatio <= 0)
				{
					EmitSignal(SignalName.FadedOut);
					_canFade = false;
				}
			}
			else // Fading in
			{
				if (VisibleRatio >= 1f)
				{
					_canFade = false;

					// Full loading animation is done so we switch scenes
					SceneManager.GoToSetScene(this);
				}
			}

			// Type out frog accordingly
			NewVisRatio += (float)delta;
			VisibleRatio = NewVisRatio;
		}
	}

	// Easy signal transfers to switch fading type
	private void FadeIn()
	{
		_fadeOut = false;
		_canFade = true;
	}
}
