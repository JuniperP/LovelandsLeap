using Godot;

/*
	Quick note:
	For continuity, the background color of this panel should be
	the same as the "almost_black_backdrop" scene.

	I tried to use that scene as the background to carry this script
	after I created it, but every time I try this, everything breaks.

	Thus, just make sure the colors line up please :).
*/

public partial class LoadingScreen : Toggleable
{
	// Stating whether we can fade or not
	public static bool AllowFade = false;

	// Which direction to fade
	public static bool IsFadeOut = true;

	// Tracking fade transition
	public static float TransTheFade = 0;

	// Signal to say we have completed fades
	[Signal] public delegate void FadedInEventHandler();
	[Signal] public delegate void FadedOutEventHandler();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Open();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// If allowed to fade, fades as needed
		if (AllowFade)
		{
			// Ensuring the user can't click around
			MouseFilter = MouseFilterEnum.Stop;

			// Fading out
			if (IsFadeOut)
			{
				delta *= -1f;
				if (TransTheFade <= 0)
				{
					AllowFade = false;
					MouseFilter = MouseFilterEnum.Ignore;
					EmitSignal(SignalName.FadedOut);
				}

			}
			// Fading in
			else
			{
				if (TransTheFade >= 1f)
				{
					AllowFade = false;
					EmitSignal(SignalName.FadedIn);
				}
			}

			TransTheFade += (float)delta;
		}

		// Changing the fade accordingly
		SelfModulate = new Color(1f, 1f, 1f, TransTheFade);
	}

	// Easy signal transfers to switch fading types
	// Fades the background out
	private void FadeOut()
	{
		IsFadeOut = true;
		AllowFade = true;
	}

	// Used to fade into black so scene can change behind the curtain
	public static void FadeIn()
	{
		IsFadeOut = false;
		AllowFade = true;
	}
}
