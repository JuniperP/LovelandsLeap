using Godot;

public partial class GlobalScreenEffects : WorldEnvironment
{
	public void SetBrightness(float newVal)
	{
		Environment.AdjustmentBrightness = newVal;
	}
}
