using Godot;

public partial class LoadingScreen : Toggleable
{
	// Stating whether we can fade or not
	private bool _canFade;

	// Which direction to fade
	private bool _fadeOut;

	// Tracking fade transition
	public static float trans = 0;

	// Signal to say we have faded in
	[Signal] public delegate void FadedInEventHandler();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Open();
		_canFade = false;
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
				if (trans <= 0)
					_canFade = false;	
			}

			else
			{
				if (trans >= 1f)
				{
					EmitSignal(SignalName.FadedIn);
					_canFade = false;
				}
			}

			trans += (float)delta;

		}

		// Changing the fade accordingly
		SelfModulate = new Color(0, 0, 0, trans);
	}

	// Easy signal transfers to switch fading types
	// Fades the background out
	private void FadeOut()
	{
		_fadeOut = true;
		_canFade = true;
	}

	// Used to fade into black so scene can change behind the curtain
	private void FadeIn()
	{
		_fadeOut = false;
		_canFade = true;
	}

}