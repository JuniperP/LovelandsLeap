using Godot;

public partial class Camera : Camera2D
{
	// Target that the camera follows
	[Export] public Node2D Target;

	// Limits are the furthest pixels the camera will see
	[ExportGroup("CustomLimits")]
	[Export] public int Left = -10000000;
	[Export] public int Top = -10000000;
	[Export] public int Right = 10000000;
	[Export] public int Bottom = 10000000;

	public override void _Process(double delta)
	{
		Vector2 targetPos = Target.GlobalPosition;

		// Half the (length, height) vector of the screen
		Vector2 camRadii = GetViewportRect().Size / 2;

		// The top left and bottom right positions allowed for the camera
		Vector2 minPos = new(Left + camRadii.X, Top + camRadii.Y);
		Vector2 maxPos = new(Right - camRadii.X, Bottom - camRadii.Y);

		Position = targetPos.Clamp(minPos, maxPos);
	}
}
