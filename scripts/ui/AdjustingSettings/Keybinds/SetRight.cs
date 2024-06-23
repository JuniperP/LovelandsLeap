using Godot;
using System;

public partial class SetRight : InputGetter
{
	protected override void ChangeValue(InputEvent ourInput)
	{
		if(Visible)
		{
			Keybinds.RightSym = ourInput.AsText();
		}
		
	}

}
