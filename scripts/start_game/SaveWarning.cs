using Godot;

public partial class SaveWarning : FadeIn
{
	// Seeing if should start fading out
	private bool _start;

	// Tracking fade transition
	private static float _trans = 0;


	// Signal to alert manager game loader that everything is set
	[Signal] public delegate void WarningDoneEventHandler(bool input);


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (CanFade && Once)
		{
			// Fade in
			if (_start)
			{
				if (_trans >= 1.5f)
				{
					_start = false;
				}

				else
					_trans += (float)delta / 2;
			}
			// Fade out
			else
			{
				if (_trans <= 0)
				{
					EmitSignal(SignalName.WarningDone, NotHeld);
					Once = false;
				}
				else
					_trans -= (float)delta / 2;
			}

		}


		// Changing the fade accordingly
		SelfModulate = new Color(1f, 1f, 1f, _trans);

	}

	// Extra setup for ready
	protected override void FurtherSetUp()
	{
		_start = true;
	}

	// Extra preparation for fade in
	protected override void FadeSetUp(bool input)
	{
		// Currently no extra setup with sfx or waiting for fades (may change later)
	}

	// Instantly loads in warning
	protected override void InstantFade()
	{
		_start = false;
		_trans = 0;
	}


}
