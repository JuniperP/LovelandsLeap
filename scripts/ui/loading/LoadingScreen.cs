using Godot;

public partial class LoadingScreen : Toggleable
{
	// Stating whether we can fade or not
	public static bool AllowFade = false;

	// Which direction to fade
	public static bool IsFadeOut = true;

	// Tracking fade transition
	public static float TransTheFade = 0;

	// Signal to say we have faded in
	[Signal] public delegate void FadedInEventHandler();


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
				}

			}
			// Fading in
			else
			{
				if (TransTheFade >= 1f)
				{
					EmitSignal(SignalName.FadedIn);
					AllowFade = false;
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
