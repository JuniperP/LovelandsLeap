// using Godot;
// using System;

// public partial class SetClick : InputGetter
// {
// 	protected override void ChangeValue(InputEvent OurInput)
// 	{
// 		if (Visible)
// 		{
// 			// Gets rid of all other keybinds
// 			InputMap.ActionEraseEvents("primary_click");

// 			// Updates current mapped button
// 			Keybinds.ClickSym = OurInput.AsText();

// 			// Updates newest event stored in action 
// 			Keybinds.ClickIn = OurInput;

// 			// Maps wanted button
// 			InputMap.ActionAddEvent("primary_click", Keybinds.ClickIn);



// 		}

// 	}
// }
