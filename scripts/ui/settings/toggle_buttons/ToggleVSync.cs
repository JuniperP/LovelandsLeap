using Godot;

public partial class ToggleVSync : ToggleButton
{
	public static bool UseVsync = true;

	// Turning off and on full screen
	public override void Toggle()
	{
		AdjustVsync();
	}

	// Getters and setters
	protected override bool GetState()
	{
		return UseVsync;
	}

	protected override void SetState(bool state)
	{
		UseVsync = state;
	}

	// Helper func to switch around Vsync
	public static void AdjustVsync()
	{
		// Switch in or out of full screen
		if (UseVsync)
			DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Enabled);
		else
			DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Disabled);

	}

}
