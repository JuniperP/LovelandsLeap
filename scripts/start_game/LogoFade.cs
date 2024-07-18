using Godot;

public partial class LogoFade : FadeIn
{
	// Signal to alert manager game loader that everything is set
	[Signal] public delegate void LogoFadedInEventHandler();


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Emits done if fully faded in logo
		if (VisibleRatio == 1 && Once)
		{
			// End transition with sound effect
			SoundManager.PlaySound(SFX.TongueHit, this, false);

			// Emit that we hit the end
			EmitSignal(SignalName.LogoFadedIn);

			// Completed goal
			Once = false;
		}

		// Slowly brings in logo if aloud
		if (CanFade)
			VisibleRatio += (float)delta / 2;

		// Ensuring if nothing is pressed at any moment, the fade can be skipped 
		if (!Input.IsAnythingPressed())
			NotHeld = true;
	}


	// Extra setup for ready
	protected override void FurtherSetUp()
	{
		VisibleRatio = 0;
	}

	// Further set up for the fade
	protected override void FadeSetUp(bool input)
	{
		// Having start of tongue shoot for fade in
		SoundManager.PlaySound(SFX.TongueShoot, this, false);

	}

	// Instantly loads in logo
	protected override void InstantFade()
	{
		VisibleRatio = .9999999f;
	}

}
