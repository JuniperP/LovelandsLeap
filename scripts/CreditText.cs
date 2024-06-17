using Godot;
using System;

public partial class CreditText : Label
{
	// Used simply for styling the ending of the credits
	int CoolEnd = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Checks if at point in credits to cut the scrolling
		if (CoolEnd != 2000)
		{
			// Scrolls and updates
			Position = new Vector2(Position.X, Position.Y - 2);
			CoolEnd++;
		}

	}
}
