using Godot;
using System;

public partial class SetLeft: InputGetter
{
	protected override void ChangeValue(InputEvent ourInput)
	{
		if(Visible)
		{
			Keybinds.LeftSym  = ourInput.AsText();
		}
		
	}
}
