using Godot;

public partial class ToggleClassicVerburg : ToggleButton
{
	// Doesn't need explanation
	public static bool Classic = false;

	// It's a secret what this method does
	public override void Toggle()
	{
		//👽
		GD.Print("👽");
	}

	// Getters and setters for IMPORTANT PURPOSES
	protected override bool GetState()
	{
		return Classic;
	}

	protected override void SetState(bool state)
	{
		Classic = state;
	}
}
