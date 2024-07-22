using Godot;

public partial class ToggleFullScreen : ToggleButton
{
	public static bool full = true;

	// Turning off and on full screen
	public override void Toggle()
	{
		// Switch in or out of full screen
		if (full)
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.ExclusiveFullscreen);
		else
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);

	}

	protected override bool GetState()
	{
		return full;
	}

	protected override void SetState(bool state)
	{
		full = state;
	}


}
