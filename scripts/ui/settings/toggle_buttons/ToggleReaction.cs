using Godot;

public partial class ToggleReaction : ToggleButton
{
	// Whether or not to have the reaction on
	public static bool HaveReaction = false;


	// Signal to create frog reaction
	[Signal] public delegate void ChangedFrogEventHandler();

	// Toggling the desired effect
	public override void Toggle()
	{
		EmitSignal(SignalName.ChangedFrog);
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
