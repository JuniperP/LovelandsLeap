using Godot;
using System;

// Static class holding all of the names each button maps to along with their events
public partial class Keybinds : Node
{
	public static InputEvent LeftIn = InputMap.ActionGetEvents("move_left")[0];
	public static String LeftSym = LeftIn.AsText();


	public static InputEvent RightIn = InputMap.ActionGetEvents("move_right")[0];
	public static String RightSym = RightIn.AsText();



	public static InputEvent JumpIn = InputMap.ActionGetEvents("move_up")[0];
	public static String JumpSym = JumpIn.AsText();


	public static InputEvent ClickIn = InputMap.ActionGetEvents("primary_click")[0];
	public static String ClickSym = ClickIn.AsText();


	public static InputEvent CancelIn = InputMap.ActionGetEvents("ui_cancel")[0];
	public static String CancelSym = CancelIn.AsText();

}
