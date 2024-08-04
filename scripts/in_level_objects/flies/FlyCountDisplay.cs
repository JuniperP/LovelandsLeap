using Godot;
using System;

public partial class FlyCountDisplay : Panel
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Updating the results every time for every time visibility changes
	public void UpdateText()
	{
		// Updating the current levels text
		Label level = GetNode<Label>("CurrentCountLevel");


		// Updating the total numbers text
		Label total = GetNode<Label>("CurrentCountInTotal");
		total.Text = $"Total: {FlyCount.FliesGottenTotal}/{FlyCount.TotalFlies}";
	}
}
