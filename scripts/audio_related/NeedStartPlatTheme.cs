using Godot;

public partial class NeedStartPlatTheme : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Since we use C# instead of GD and autoload just throws item to root...
		GetNode<MainPlatformingTheme>("/root/MainPlatformingThemeStream").Play();
	}
}
