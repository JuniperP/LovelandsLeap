using Godot;

public partial class FlyLeaf : Control
{
	[Export] public Label InLevel;
	[Export] public Label Total;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Updating the current levels text
		InLevel.Text = $"{FlyCount.FliesGottenLevel} / {FlyCount.TotalLevelFlies}";

		// Updating the total numbers text
		Total.Text = $"{FlyCount.FliesGottenLevelTotal} / {FlyCount.TotalGameFlies}";
	}
}
