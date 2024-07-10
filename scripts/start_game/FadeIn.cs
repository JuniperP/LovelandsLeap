using Godot;
using System;

public abstract partial class FadeIn : RichTextLabel
{
	// Boolean to track if process can start fading
	protected bool CanFade;

	// Only emit the signal once
	protected bool Once;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CanFade = false;
		Once = true;
		FurtherSetUp();
	}

	// Starts bringing in logo if told to
	protected void CanNowFade()
	{
		// Saying fading is now allowed
		CanFade = true;

		// Start transition with sound effect
		SFXSetUp();
	}

	// Extra sub class setup
	protected abstract void FurtherSetUp();
	// The sound effect to play when allowed to fade in
	protected abstract void SFXSetUp();
}
