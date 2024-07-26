using Godot;

public partial class LoadingScreen : Toggleable
{
	// Stating whether we can fade or not
	public static bool canFade = false;

	// Which direction to fade
	public static bool fadeOut = true;

	// Tracking fade transition
	public static float trans = 0;

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
		if (canFade)
		{
			// Ensuring the user can't click around
			MouseFilter = MouseFilterEnum.Stop;

			// Fading out
			if (fadeOut)
			{
				delta *= -1f;
				if (trans <= 0)
				{
					canFade = false;
					MouseFilter = MouseFilterEnum.Ignore;
				}

			}
			// Fading in
			else
			{
				if (trans >= 1f)
				{
					EmitSignal(SignalName.FadedIn);
					canFade = false;
				}
			}

			trans += (float)delta;
		}


		// Changing the fade accordingly
		SelfModulate = new Color(0, 0, 0, trans);
	}

	// Easy signal transfers to switch fading types
	// Fades the background out
	private void FadeOut()
	{
		fadeOut = true;
		canFade = true;
	}

	// Used to fade into black so scene can change behind the curtain
	public static void FadeIn()
	{
		fadeOut = false;
		canFade = true;
	}

}