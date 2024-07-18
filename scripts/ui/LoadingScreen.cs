using Godot;

// I know its kinda funky 
public partial class LoadingScreen : Panel
{
	// Stating whether we can fade or not
	private bool _canFade;

	// Which direction to fade
	private bool _fadeOut;

	// Tracking fade transition
	private static float _trans = 0;


	// Signal to say we have faded out
	[Signal] public delegate void FadedOutEventHandler();
	// Signal to say we have faded in
	[Signal] public delegate void FadedInEventHandler();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartFade(false);
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

				if (_trans <= 0)
				{
					EmitSignal(SignalName.FadedOut);
					_canFade = false;
				}
			}

			else
			{
				if (_trans >= 1f)
				{
					EmitSignal(SignalName.FadedIn);
					_canFade = false;
				}
			}

			_trans += (float)delta;

		}

		// Changing the fade accordingly
		SelfModulate = new Color(0, 0, 0, _trans);

	}

	// Easy signal transfers to switch fading type
	private void StartFade(bool fadeOut)
	{
		_fadeOut = fadeOut;
		_canFade = true;
	}

}