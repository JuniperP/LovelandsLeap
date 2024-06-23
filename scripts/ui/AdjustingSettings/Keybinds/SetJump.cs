using Godot;
using System;

public partial class SetJump : InputGetter
{
	protected override void ChangeValue(InputEvent ourInput)
	{
		if(Visible)
		{
			Keybinds.JumpSym = ourInput.AsText();
		}
		
	}
}
