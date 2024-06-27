using Godot;
using System;

public partial class SetKeyBinds : Toggleable
{
	// What we are mapping to (meant to be set by the parent of the key bind set combo)
	public String Mapping = "";

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
			InputMap.ActionEraseEvents(Mapping);


			// Updates current mapped button label and the stored action
			if (Mapping == "move_left")
			{
				Keybinds.LeftSym = OurInput.AsText();
				Keybinds.LeftIn = OurInput;
			}
			else if (Mapping == "move_right")
			{
				Keybinds.RightSym = OurInput.AsText();
				Keybinds.RightIn = OurInput;
			}
			else if (Mapping == "move_up")
			{
				Keybinds.JumpSym = OurInput.AsText();
				Keybinds.JumpIn = OurInput;
			}
			else if (Mapping == "primary_click")
			{
				Keybinds.ClickSym = OurInput.AsText();
				Keybinds.ClickIn = OurInput;
			}



			// Maps wanted button
			InputMap.ActionAddEvent(Mapping, OurInput);


		}
	}
}
