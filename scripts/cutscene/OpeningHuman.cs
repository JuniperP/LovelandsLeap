using Godot;
using System;

public partial class OpeningHuman : MultiAnimation2D
{
	[Export] public float WalkSpeed = 300f;
	private bool _isWalking;

	public override void _Process(double delta)
	{
		if (_isWalking)
		{
			Position = new Vector2(
				Position.X + WalkSpeed * (float)delta,
				Position.Y
			);
		}
	}

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
	}

	private void WalkOff(double lifeSpan)
	{
		_isWalking = true;
		SetTimer(lifeSpan, DisableWalking);
	}

	private void DisableWalking()
	{
		_isWalking = false;
		Visible = false;
	}
}
