using Godot;

public partial class ToggleFullScreen : ToggleButton
{
	// Turning off and on full screen
	public override void Toggle()
	{
		// Switch in or out of full screen
		if (IsOn)	
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		else
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		
		// Switch IsOn
		IsOn = !IsOn;
	}


}
