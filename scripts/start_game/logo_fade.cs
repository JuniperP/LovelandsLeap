using Godot;
using System;

public partial class logo_fade : RichTextLabel
{
	// Boolean to track if process can start fading
	private bool CanFade;

	// Only emit the signal once
	private bool once;

	// Signal to alert manager game loader that everything is set
	[Signal] public delegate void LogoFadedInEventHandler();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CanFade = false;
		once = true;
		VisibleRatio = 0;
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Emits done if fully faded in logo
		if (VisibleRatio == 1 && once)
		{
			// End transition with sound effect
			SoundManager.PlaySound(SFX.TongueHit, this);

			// Emit that we hit the end
			EmitSignal(SignalName.LogoFadedIn);

			// Completed goal
			once = false;
		}
			

		// Slowly brings in logo if aloud
		if (CanFade)
			VisibleRatio += (float)delta / 2;
	}


	// Starts bringing in logo if told to
	private void CanNowFade()
	{
		CanFade = true;

		// Start transition with sound effect
		SoundManager.PlaySound(SFX.TongueShoot, this);
	}


	// Allows user to skip the opening fade in 
	public override void _Input(InputEvent OurInput)
	{
		if ((OurInput is InputEventMouseButton || OurInput is InputEventKey) && CanFade)
			VisibleRatio = .9999999f;
	}
}
