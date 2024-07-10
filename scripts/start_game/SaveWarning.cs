using Godot;

public partial class SaveWarning : FadeIn
{
	// Seeing if should start fading out
	private bool Start;

	// Tracking fade transition
	private static float Trans = 0;


	// Signal to alert manager game loader that everything is set
	[Signal] public delegate void WarningDoneEventHandler();


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (CanFade && Once)
		{
			// Fade in
			if (Start)
			{
				if (Trans >= 1)
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
					EmitSignal(SignalName.WarningDone);
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

	// Having start of tongue shoot for fade in
    protected override void SFXSetUp()
    {
        // None currently though this may change
    }

}
