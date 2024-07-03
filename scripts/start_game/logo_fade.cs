using Godot;
using System;

public partial class logo_fade : RichTextLabel
{
	// Boolean to track if process can start fading
	private bool CanFade;

	// Signal to alert manager game loader that everything is set
	[Signal] public delegate void LogoFadedInEventHandler();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CanFade = false;
		VisibleRatio = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Emits done if fully faded in logo
		if (VisibleRatio == 1)
			EmitSignal(SignalName.LogoFadedIn);

		// Slowly brings in logo if aloud
		if (CanFade)
			VisibleRatio += (float)delta / 2;
		

	}

	// Starts bringing in logo if told to
	private void CanNowFade()
	{
		CanFade = true;
	}

	// Allows user to skip the opening fade in 
	public override void _Input(InputEvent OurInput)
	{
		if ((OurInput is InputEventMouseButton || OurInput is InputEventKey) && CanFade)
			VisibleRatio = (float).9999999;
	}
}
