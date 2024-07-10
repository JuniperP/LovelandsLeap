using Godot;
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


// Static class holding all of the names each button maps to along with their events
public partial class Keybinds : Node
{
	// Dictionary for comprehensive access to each sfx
	public static readonly Dictionary<UserAction, ActionRep> _acts = new()
	{
		{UserAction.Left, new ActionRep("move_left", InputMap.ActionGetEvents("move_left")[0], "Move Left")},
		{UserAction.Right, new ActionRep("move_right", InputMap.ActionGetEvents("move_right")[0], "Move Right")},
		{UserAction.Jump, new ActionRep("move_up", InputMap.ActionGetEvents("move_up")[0], "Jump")},
		{UserAction.Down, new ActionRep("move_down", InputMap.ActionGetEvents("move_down")[0], "Fast Fall")},
		{UserAction.Click, new ActionRep("primary_click", InputMap.ActionGetEvents("primary_click")[0], "Use Tongue")},
		{UserAction.Cancel, new ActionRep("ui_cancel",InputMap.ActionGetEvents("ui_cancel")[0], "Cancel")}
	};
}
