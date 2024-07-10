using Godot;

public partial class ToggleFullScreen : ToggleButton
{
	public static bool Full = true;

	// Turning off and on full screen
	public override void Toggle()
	{
		// Switch in or out of full screen
		if (Full)
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		else
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);

	}

	protected override bool GetState()
	{
		return Full;
	}

	protected override void SetState(bool state)
	{
		Full = state;
	}


}
