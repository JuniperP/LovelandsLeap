using Godot;
using System;

public partial class TongueLabel : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = Keybinds.ClickSym;
	}

}
