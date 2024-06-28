using Godot;
using System;

public partial class ToggleFullScreen : CheckButton
{

	private void toggle(Boolean IsOn)
	{
		if (IsOn)
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}
		else
		{
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		}
	}


}
