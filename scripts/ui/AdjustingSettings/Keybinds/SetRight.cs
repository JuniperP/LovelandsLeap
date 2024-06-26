using Godot;
using System;

public partial class SetRight : InputGetter
{
	protected override void ChangeValue(InputEvent OurInput)
	{
		if (Visible)
		{
			// Gets rid of all other keybinds
			InputMap.ActionEraseEvents("move_right");

			// Updates current mapped button
			Keybinds.RightSym = OurInput.AsText();

			// Updates newest event stored in action 
			Keybinds.RightIn = OurInput;

			// Maps wanted button
			InputMap.ActionAddEvent("move_right", Keybinds.RightIn);


		}

	}

}
