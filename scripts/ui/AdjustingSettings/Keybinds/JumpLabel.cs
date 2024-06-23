using Godot;
using System;

public partial class JumpLabel : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = Keybinds.JumpSym;
	}

}
