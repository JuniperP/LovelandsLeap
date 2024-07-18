using Godot;

public partial class AsciiFrog : RichTextLabel
{
	// Stating whether we can fade or not
	private bool _canFade;

	// Which direction to fade
	private bool _fadeOut;

	// Tracking fade transition
	private static float _trans = 0;

	// Signal to say we have faded out
	[Signal] public delegate void FadedOutEventHandler();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (_trans >= 1)
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

				if (_trans <= 0)
				{
					EmitSignal(SignalName.FadedOut);
					_canFade = false;
				}
			}

			else
			{
				if (_trans >= 1f)
					_canFade = false;
			}

			_trans += (float)delta;


			// Changing the fade accordingly
			SelfModulate = new Color(0, 0, 0, _trans);

		}
	}

	// Easy signal transfers to switch fading type
	private void FadeIn()
	{
		_fadeOut = false;
		_canFade = true;
	}

}
