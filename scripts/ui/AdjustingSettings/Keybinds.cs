using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Keybinds : Node
{
	// Variables holding the godot ui commands to activate that action
	public static String MoveLeft = "ui_left";
	public static String LeftSym = "←";

	public static String MoveRight =  "ui_right";
	public static String RightSym =  "→";

	public static String Jump = "ui_up";
	public static String JumpSym =  "↑";

	public static String UseTounge = "primary_click";
	public static String ClickSym = "R_Click";




	public static void SetLeft(InputEventKey input)
	{
		
		//GD.Print(OS.GetKeycodeString(input.GetKeycodeWithModifiers()));

	}

}
