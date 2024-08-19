using Godot;

public partial class ButtonPlatform : AnimatableBody2D
{
	// Signals to notifying the rest of scene of button changes
	[Signal] public delegate void ButtonPressedEventHandler();
	[Signal] public delegate void ButtonReleasedEventHandler();

	// Easy signal chaining from button on platform to track system
	private void PlatformButtonPressed()
	{
		EmitSignal(SignalName.ButtonPressed);
	}

	private void PlatformButtonReleased()
	{
		EmitSignal(SignalName.ButtonReleased);
	}
}
