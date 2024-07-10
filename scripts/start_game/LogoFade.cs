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
			SoundManager.PlaySound(SFX.TongueHit, this);

			// Emit that we hit the end
			EmitSignal(SignalName.LogoFadedIn);

			// Completed goal
			Once = false;
		}	

		// Slowly brings in logo if aloud
		if (CanFade)
			VisibleRatio += (float)delta / 2;
	}


	// Extra setup for ready
	protected override void FurtherSetUp()
	{
		VisibleRatio = 0;
	}

	// Having start of tongue shoot for fade in
    protected override void SFXSetUp()
    {
        SoundManager.PlaySound(SFX.TongueShoot, this);
    }


    // Allows user to skip the opening fade in 
    public override void _Input(InputEvent OurInput)
	{
		if ((OurInput is InputEventMouseButton || OurInput is InputEventKey) && CanFade)
			VisibleRatio = .9999999f;
	}
}
