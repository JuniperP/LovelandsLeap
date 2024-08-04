using Godot;

public partial class FlyCountDisplay : Panel
{
	// How we will count up to 3
	private double count;

	// Checking if we 
	[Export] private bool _isInLevel;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		count = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_isInLevel)
		{
			if (GetTree().Paused)
				Hide();

			else
			{
				if (!Visible && count >= 2)
					Show();

				else if (Visible && count < 2)
					Hide();

				if (Input.IsAnythingPressed() == false)
					count += delta;

				else
					count = 0;
			}

		}

		else
		{
			if (!Visible)
				Show();
		}

	}

	// Updating the results every time for every time visibility changes
	public void UpdateText()
	{
		// Updating the current levels text
		Label level = GetNode<Label>("CurrentCountLevel");
		level.Text = $"Level: {FlyCount.FliesGottenLevel}/{FlyCount._numFlies[SceneManager.GetNextGoTo()]}";


		// Updating the total numbers text
		Label total = GetNode<Label>("CurrentCountInTotal");
		total.Text = $"Total: {FlyCount.FliesGottenTotal}/{FlyCount.TotalFlies}";
	}
}
