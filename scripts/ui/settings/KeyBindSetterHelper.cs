using Godot;
using System;


public partial class KeyBindSetterHelper : Node
{

	// Sets the key bind for an action in the game given a action to replace and user input
	public static void SetKeyBind(InputEvent OurInput, UserAction OurAction)
	{

		// Gets rid of all other key binds
		InputMap.ActionEraseEvents(Keybinds._acts[OurAction].Mapping);

		// Updates current stored key to click for that action's key bind
		Keybinds._acts[OurAction].Input = OurInput;

		// Maps wanted button
		InputMap.ActionAddEvent(Keybinds._acts[OurAction].Mapping, Keybinds._acts[OurAction].Input);
	}
}
