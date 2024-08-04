using Godot;

public partial class CurrentCountLabel : Label
{
	// Updating the results every time for every time visibility changes
	public void UpdateText()
	{
		Text = $"Total: {FlyCount.FliesGotten}/{FlyCount.TotalFlies}";
	}
}
