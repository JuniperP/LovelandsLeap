using Godot;

public partial class SaveWarning : FadeIn
{
	// Seeing if should start fading out
	private bool Start;

	// Tracking fade transition
	private static float Trans = 0;


	// Signal to alert manager game loader that everything is set
	[Signal] public delegate void WarningDoneEventHandler(bool input);


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (CanFade && Once)
		{
			// Fade in
			if (Start)
			{
				if (Trans >= 1.5f)
				{
					Start = false;
				}

				else
					Trans += (float)delta / 2;
			}
			// Fade out
			else
			{
				if (Trans <= 0)
				{
					EmitSignal(SignalName.WarningDone, NotHeld);
					Once = false;
				}
				else
					Trans -= (float)delta / 2;
			}

		}


		// Changing the fade accordingly
		SelfModulate = new Color(1f, 1f, 1f, Trans);

	}

	// Extra setup for ready
	protected override void FurtherSetUp()
	{
		Start = true;
	}

	// Extra preparation for fade in
	protected override void FadeSetUp(bool input)
	{
		// Currently no extra setup with sfx or waiting for fades (may change later)
	}

	// Instantly loads in warning
	protected override void InstantFade()
	{
		Start = false;
		Trans = 0;
	}


}
