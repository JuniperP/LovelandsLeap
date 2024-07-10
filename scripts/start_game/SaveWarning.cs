using Godot;

public partial class SaveWarning : RichTextLabel
{
	// Boolean to track if process can start fading
	private bool CanFade;

	// Only emit the signal once
	private bool once;

	// Seeing if should start fading out
	private bool Start;

	// Tracking fade transition
	private static float Trans = 0;


	// Signal to alert manager game loader that everything is set
	[Signal] public delegate void WarningDoneEventHandler();


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (CanFade && once)
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
					once = false;
				}
				else
					Trans -= (float)delta / 2;
			}

		}


		// Changing the fade accordingly
		SelfModulate = new Color(1f, 1f, 1f, Trans);

	}

	// Starts bringing in logo if told to
	private void CanNowFade()
	{
		CanFade = true;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CanFade = false;
		once = true;
		Start = true;
	}
}
