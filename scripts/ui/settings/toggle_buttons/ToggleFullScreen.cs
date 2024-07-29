using Godot;

public partial class ToggleFullScreen : ToggleButton
{
	public static bool IsFull = true;

	// Turning off and on full screen
	public override void Toggle()
	{
		// Switch in or out of full screen
		if (IsFull)
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
		else
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
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
}
