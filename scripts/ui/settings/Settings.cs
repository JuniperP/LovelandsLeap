using Godot;
using System;


public partial class Settings : Toggleable
{

	// Boolean to check if the user is holding down the button they intend to escape with 
	public Boolean HeldDown;

	// Setup by closing the visibility and prepping our held down check
	public override void _Ready()
	{
		_close();
		HeldDown = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		// Letting the user quit from settings
		if (Input.IsActionPressed("ui_cancel") && !HeldDown)
		{
			_close();
			HeldDown = true;
		}

		// If the escape key isn't being pressed, it is deemed not held down
		else if (!Input.IsActionPressed("ui_cancel") && HeldDown)
			HeldDown = false;

	}

}
