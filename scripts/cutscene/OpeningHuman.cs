using Godot;
using System;

public partial class OpeningHuman : MultiAnimation2D
{
	protected override Action<double>[] SetupAnimations()
	{
		return new Action<double>[]{
			Appear,
			WalkOff
		};
	}

	private void Appear(double lifeSpan)
	{
		Visible = true;
		SetTimer(lifeSpan, () => Visible = false);
	}

	private void WalkOff(double lifeSpan)
	{
		GD.Print("Walking off!");
	}
}
