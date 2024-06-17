using Godot;
using System;

public partial class Settings : Control
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

	//Open the menu
	private void _open()
	{
		Visible=true;
	}
}
