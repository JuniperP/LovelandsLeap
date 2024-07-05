using Godot;
using System;

public partial class InfoToSkip : Label
{
	public override void _Ready()
	{
		Text = $"Press [{Keybinds._acts[UserAction.Cancel].Input.AsText()}] to skip";
	}
}
