using Godot;

public partial class ToggleFullScreen : ToggleButton
{
	public static bool IsFull = true;

	// Turning off and on full screen
	public override void Toggle()
	{
		AdjustWindowLocation();
	}

	// Getters and setters
	protected override bool GetState()
	{
		return IsFull;
	}

	protected override void SetState(bool state)
	{
		IsFull = state;
	}

	// Helper func to change the window
	public static void AdjustWindowLocation()
	{
		// Switch in or out of full screen
		if (IsFull)
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
		else
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);

	}
}
