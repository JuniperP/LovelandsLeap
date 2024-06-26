using Godot;
using System;
//using System.Runtime.CompilerServices;

public partial class Keybinds : Node
{
	// Variables holding all of the names each mapped button along with their events
	public static InputEvent LeftIn = InputMap.ActionGetEvents("move_left")[0];
	public static String LeftSym = LeftIn.AsText();


	public static InputEvent RightIn = InputMap.ActionGetEvents("move_right")[0];
	public static String RightSym = RightIn.AsText();



	public static InputEvent JumpIn = InputMap.ActionGetEvents("move_up")[0];
	public static String JumpSym = JumpIn.AsText();


	public static InputEvent ClickIn = InputMap.ActionGetEvents("primary_click")[0];
	public static String ClickSym = ClickIn.AsText();


}
