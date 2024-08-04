using Godot;

public partial class FlyCountDisplay : Panel
{
	// How we will count up to 3
	private double _count;

	private float _currentFade;

	// Checking if we 
	[Export] private bool _isInLevel;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_count = 0;
		_currentFade = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_isInLevel)
		{
			if (GetTree().Paused)
				Modulate = new Color(1, 1, 1, 0);

			else
			{
				if (_count >= 2)
					_currentFade += (float)delta;

				if (Input.IsAnythingPressed() == false)
					_count += delta;

				else
				{
					_count = 0;
					_currentFade = 0;
				}

				Modulate = new Color(1, 1, 1, _currentFade);
			}

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
