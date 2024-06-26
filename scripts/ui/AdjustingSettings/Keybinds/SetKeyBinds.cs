using Godot;
using System;

public partial class SetKeyBinds : Toggleable
{
	[Export] String mapping = "move_up";

	// Close on loading in
	public override void _Ready()
	{
		_close();
	}

	// Checks for any input and if a valid input is given it is sent to change our key binds
	public override void _Input(InputEvent OurInput)
	{
		if (OurInput is InputEventMouseButton || OurInput is InputEventKey)
		{
			ChangeValue(OurInput);
			_close();
		}

	}

	// What values are precisely changed are to be based on the sub class
	protected void ChangeValue(InputEvent OurInput)
	{
		if (Visible)
		{
			// Gets rid of all other keybinds
			InputMap.ActionEraseEvents(mapping);


			// Updates current mapped button label and the stored action
			if (mapping == "move_left")
			{
				Keybinds.LeftSym = OurInput.AsText();
				Keybinds.LeftIn = OurInput;
			}
			else if (mapping == "move_right")
			{
				Keybinds.RightSym = OurInput.AsText();
				Keybinds.RightIn = OurInput;
			}
			else if (mapping == "move_up")
			{
				Keybinds.JumpSym = OurInput.AsText();
				Keybinds.JumpIn = OurInput;
			}
			else
			{
				Keybinds.ClickSym = OurInput.AsText();
				Keybinds.ClickIn = OurInput;
			}



			// Maps wanted button
			InputMap.ActionAddEvent(mapping, OurInput);


		}
	}
}
