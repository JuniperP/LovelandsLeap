using Godot;

public partial class CurrentCountTotal : Label
{
	// Updating the results every time for every time visibility changes
	public void UpdateText()
	{
		Text = $"Total: {FlyCount.FliesGottenTotal}/{FlyCount.TotalFlies}";
	}
}
