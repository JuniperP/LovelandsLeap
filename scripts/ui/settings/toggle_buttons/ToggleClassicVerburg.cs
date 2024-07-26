public partial class ToggleClassicVerburg : ToggleButton
{
	// Doesn't need explanation
	public static bool classic = false;

	// It's a secret what this method does
	public override void Toggle()
	{
		//👽
	}

	// Getters and setters for IMPORTANT PURPOSES
	protected override bool GetState()
	{
		return classic;
	}

	protected override void SetState(bool state)
	{
		classic = state;
	}

}
