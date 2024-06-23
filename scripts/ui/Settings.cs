using Godot;
using System;
using System.Diagnostics.Metrics;

public partial class Settings : Toggleable
{
	// Counter to ensure pause screen can't be closed the next frame
	public double counter;

	// Setup
	public override void _Ready()
	{
		_close();
		counter = 0;
	}

	public override void _Process(double delta)
	{
		// Count since settings has been closed
		if(counter<=.3 && !Visible)
		{
			counter+=delta;
		}

		// Letting the user quit from settings and resetting counter
		if (Input.IsActionPressed("ui_cancel") && Visible)
		{
			_close();
			counter=0;
		}
		
	}
	
}
