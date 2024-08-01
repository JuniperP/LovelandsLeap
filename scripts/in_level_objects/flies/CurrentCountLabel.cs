using Godot;

public partial class CurrentCountLabel : Label
{
	// The second half of the label saying making the count fraction
	private string _outOf;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_outOf = $"/{FlyCount.TotalFlies}";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Making the label show the current fraction of flies gotten
		if (Visible)
			Text = FlyCount.FliesGotten + _outOf;
	}
}
