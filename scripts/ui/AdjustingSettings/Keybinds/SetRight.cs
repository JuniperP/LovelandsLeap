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

			// Releasing the button just in case to ensure Godot isn't confused about adding just removed items
			Input.ActionRelease("move_right");//		-	Not needed as we are operating under only one key per option for now

			// Updates current mapped button
			Keybinds.RightSym = OurInput.AsText();

			// Updates newest event stored in action 
			Keybinds.RightIn = OurInput;

			// Maps wanted button
			InputMap.ActionAddEvent("move_right", Keybinds.RightIn);


		}

	}

}
