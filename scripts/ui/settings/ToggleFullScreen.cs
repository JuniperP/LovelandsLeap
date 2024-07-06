using Godot;
using System;

public partial class ToggleFullScreen : Button
{
	// To see if in full screen or not
	public static bool IsOn = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(IsOn)
			Text = "On";
		else
			Text = "Off";
	}
	
	// Turning off and on full screen
	public static void Toggle()
	{
		// Switch in or out of full screen
		if (IsOn)	
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		else
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		
		// Switch IsOn
		IsOn = !IsOn;
	}


	private void _adjust_name_and_screen()
	{
		// Change what the button displays
		if(IsOn)
			Text = "Off";
		else
			Text = "On";
		
		// Change what 
		Toggle();
		
	}
}
