using Godot;
using System;

public partial class InfoToSkip : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = "Press ["+Keybinds.CancelSym+"] to skip";
	}

}
