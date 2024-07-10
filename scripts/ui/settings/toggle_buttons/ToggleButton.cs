using Godot;

public abstract partial class ToggleButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (GetState())
			Text = "On";
		else
			Text = "Off";
	}


	// Letting the button switch its label on and off
	protected void _adjust_name_and_screen()
	{
		// Change what the button displays
		if (GetState())
			Text = "Off";
		else
			Text = "On";

		// Switch IsOn
		SetState(!GetState());

		// Change effects accordingly
		Toggle();

	}


	// Toggling the desired effect
	public abstract void Toggle();

	// All subclasses must have getters and setters for their respected static states
	protected abstract void SetState(bool b);
	protected abstract bool GetState();
}
