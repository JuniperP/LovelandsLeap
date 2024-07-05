using Godot;
using System;

public partial class InfoToSkip : Label
{
	public override void _Ready()
	{
		Text = $"Press [{Keybinds.CancelIn.AsText()}] to skip";
	}
}
