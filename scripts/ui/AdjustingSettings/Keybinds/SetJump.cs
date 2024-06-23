using Godot;
using System;

public partial class SetJump : InputGetter
{
	protected override void ChangeValue(InputEvent OurInput)
	{
		if(Visible)
		{
			// Gets rid of all other keybinds
			InputMap.ActionEraseEvents("move_up");

			// Updates current mapped button
			Keybinds.JumpSym = OurInput.AsText();

			// Maps wanted button
			InputMap.ActionAddEvent("move_up", OurInput);


		}
		
	}
}
