public partial class ToggleReaction : ToggleButton
{
	// Whether or not to have the reaction on
	public static bool HaveReaction = false;

	// Toggling the desired effect
	public override void Toggle()
	{
		// Nothing more at the moment
	}

	// Needed getters and setters
	protected override void SetState(bool ourState)
	{
		HaveReaction = ourState;
	}
	protected override bool GetState()
	{
		return HaveReaction;
	}
}
