using Godot;

public partial class CreditText : Label
{

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Checks if at point in credits to cut the scrolling
		if (Position.Y > -5400)
		{
			// Scrolls and updates
			Position = new Vector2(Position.X, Position.Y - 2);

		}

	}
}
