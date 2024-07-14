using Godot;

public abstract partial class ToggleButton : Button
{
	// Update whether the box says on or off (but only when seen)
	public void UpdateText()
	{
		if (Visible)
		{
			if (GetState())
				Text = "On";
			else
				Text = "Off";
		}

	}


	// Letting the button switch its label on and off
	protected void AdjustNameAndScreen()
	{
		// Switch state
		SetState(!GetState());

		// Change effects accordingly
		Toggle();

		// Change what the button displays
		UpdateText();

	}


	// Toggling the desired effect
	public abstract void Toggle();

	// All subclasses must have getters and setters for their respected static states
	protected abstract void SetState(bool ourState);
	protected abstract bool GetState();
}
