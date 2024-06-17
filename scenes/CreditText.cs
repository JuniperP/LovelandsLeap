using Godot;
using System;

public partial class CreditText : Label
{
	int CoolEnd = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (CoolEnd != 2000)
		{
			Position = new Vector2(Position.X, Position.Y - 2);
			CoolEnd++;
		}
		GD.Print(CoolEnd);
		
	}
}
