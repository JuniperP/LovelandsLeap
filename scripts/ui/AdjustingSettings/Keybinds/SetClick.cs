using Godot;
using System;

public partial class SetClick : InputGetter
{
	protected override void ChangeValue(InputEvent ourInput)
	{
		if(Visible)
		{
			Keybinds.ClickSym = ourInput.AsText();
		}
		
	}
}
