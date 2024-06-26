using Godot;
using System;

public partial class SetLeft : InputGetter
{
	protected override void ChangeValue(InputEvent OurInput)
	{
		if (Visible)
		{
			// Gets rid of all other keybinds
			InputMap.ActionEraseEvents("move_left");

			// Releasing the button just in case to ensure Godot isn't confused about adding just removed items
			Input.ActionRelease("move_left");//		-	Not needed as we are operating under only one key per option for now

			// Updates current mapped button
			Keybinds.LeftSym = OurInput.AsText();

			// Updates newest event stored in action 
			Keybinds.LeftIn = OurInput;

			// Maps wanted button
			InputMap.ActionAddEvent("move_left", Keybinds.LeftIn);


		}

	}
}
