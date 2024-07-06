using Godot;
using System;

public partial class ToggleFullScreen : Button
{
	// To see if in full screen or not
	private static bool IsOn = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(IsOn)
			Text = "On";
		else
			Text = "Off";
	}
	
	// Turning off and on full screen
	private void _toggle()
	{
		if (IsOn)
		{
			Text = "Off";
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		}
		else
		{
			Text = "On";
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}

		// Switch IsOn
		IsOn = !IsOn;
	}
}
