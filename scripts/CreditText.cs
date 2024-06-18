using Godot;
using System;

public partial class CreditText : Label
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Checks if at point in credits to cut the scrolling
		if (Position.Y > -4650)
		{
			// Scrolls and updates
			Position = new Vector2(Position.X, Position.Y - 2);
			
		}

	}
}
