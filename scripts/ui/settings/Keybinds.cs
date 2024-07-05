using Godot;
using System;

// Static class holding all of the names each button maps to along with their events
public partial class Keybinds : Node
{
	public static InputEvent LeftIn = InputMap.ActionGetEvents("move_left")[0];

	public static InputEvent RightIn = InputMap.ActionGetEvents("move_right")[0];

	public static InputEvent JumpIn = InputMap.ActionGetEvents("move_up")[0];

	public static InputEvent DownIn = InputMap.ActionGetEvents("move_down")[0];

	public static InputEvent ClickIn = InputMap.ActionGetEvents("primary_click")[0];

	public static InputEvent CancelIn = InputMap.ActionGetEvents("ui_cancel")[0];
}
