using Godot;
using System;
using System.Collections.Generic;


// Enum for each possible action
public enum UserAction : byte
{
	Left,
	Right,
	Jump,
	Down,
	Click,
	Cancel,
}

public partial class KeyBindSetterHelper : Node
{


	// Dictionary for comprehensive access to each sfx
	public static readonly Dictionary<UserAction, ActionRep> _acts = new()
	{
		{UserAction.Left, new ActionRep("move_left", Keybinds.LeftIn, Keybinds.LeftSym, "Move Left")},
		{UserAction.Right, new ActionRep("move_right",Keybinds.RightIn, Keybinds.RightSym, "Move Right")},
		{UserAction.Jump, new ActionRep("move_up", Keybinds.JumpIn, Keybinds.JumpSym, "Jump")},
		{UserAction.Down, new ActionRep("move_down", Keybinds.DownIn, Keybinds.DownSym, "Fast Fall")},
		{UserAction.Click, new ActionRep("primary_click", Keybinds.ClickIn, Keybinds.ClickSym, "Use Tongue")},
		{UserAction.Cancel, new ActionRep("ui_cancel", Keybinds.CancelIn, Keybinds.CancelSym, "Cancel")}
	};


	// Sets the key bind for an action in the game given a action to replace and user input
	public static void SetKeyBind(InputEvent OurInput, UserAction OurAction)
	{

		// Gets rid of all other key binds
		InputMap.ActionEraseEvents(_acts[OurAction].Mapping);

		// Updates current stored key to click for that action's key bind
		_acts[OurAction].Input = OurInput; 

		// Updates current symbol for that action's key bind
		_acts[OurAction].Symbol = OurInput.AsText();
		
		// Maps wanted button
		InputMap.ActionAddEvent(_acts[OurAction].Mapping, _acts[OurAction].Input);
	}
}
