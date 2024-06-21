using Godot;
using System;

public partial class Camera : Camera2D
{
	[Export] public Node2D Target;

	[ExportGroup("CustomLimits")]
	[Export] public int Left = -10000000;
	[Export] public int Top = -10000000;
	[Export] public int Right = 10000000;
	[Export] public int Bottom = 10000000;

	public override void _Process(double delta)
	{
		Vector2 targetPos = Target.GlobalPosition;

		// Not technically radii but half the length and width
		Vector2 camRadii = GetViewportRect().Size / 2;
		Vector2 minPos = new(Left + camRadii.X, Top + camRadii.Y);
		Vector2 maxPos = new(Right - camRadii.X, Bottom - camRadii.Y);

		Position = targetPos.Clamp(minPos, maxPos);
	}
}
