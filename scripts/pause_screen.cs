using Godot;
using System;

public partial class pause_screen : Control
{
	// Setup
	public override void _Ready()
	{
		Visible=false;
	}
	
	// Close the menu
	private void _close()
	{
		Visible=false;
	}

	// Open the menu
	private void _open()
	{
		Visible=true;
	}

	// Return to the main menu
	private void _to_main_menu()
	{
		GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
	}
}

