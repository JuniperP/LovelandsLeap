using Godot;
using System;

public partial class TongueLine : Line2D
{
	[Export] public Node2D Target;

	public override void _Process(double delta)
	{
		SetPointPosition(1, Target.Position);
	}
}
