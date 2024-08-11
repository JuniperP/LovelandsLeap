using Godot;

public partial class FlyCountDisplay : Panel
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Updating the current levels text
		Label level = GetNode<Label>("CurrentCountLevel");
		level.Text = $"Level: {FlyCount.FliesGottenLevel}/{FlyCount.TotalLevelFlies}";


		// Updating the total numbers text
		Label total = GetNode<Label>("CurrentCountInTotal");
		total.Text = $"Total: {FlyCount.FliesGottenTotal}/{FlyCount.TotalGameFlies}";
	}

}
