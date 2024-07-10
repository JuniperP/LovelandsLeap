using Godot;

public abstract partial class ToggleButton : Button
{
	// To see if in full screen or not
	public static bool IsOn = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (IsOn)
			Text = "On";
		else
			Text = "Off";
	}


	// Letting the button switch its label on and off
	protected void _adjust_name_and_screen()
	{
		// Change what the button displays
		if (IsOn)
			Text = "Off";
		else
			Text = "On";

		// Switch IsOn
		IsOn = !IsOn;

		// Change effects accordingly
		Toggle();

	}


	// Toggling the desired effect
	public abstract void Toggle();

}
