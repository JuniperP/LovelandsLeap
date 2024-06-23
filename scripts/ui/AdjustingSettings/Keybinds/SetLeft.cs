using Godot;
using System;

public partial class SetLeft: InputGetter
{
	protected override void ChangeValue(InputEvent OurInput)
	{
		if(Visible)
		{
			// Gets rid of all other keybinds
			InputMap.ActionEraseEvents("move_left");

			// Updates current mapped button
			Keybinds.LeftSym = OurInput.AsText();

			// Maps wanted button
			InputMap.ActionAddEvent("move_left", OurInput);


		}
		
	}
}
