using Godot;

// THIS SCRIPT IS FOR TESTING ONLY, IT WILL NOT EXIST IN THE FINAL PRODUCT!
public partial class Temp1stLevelScript : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MainPlatformingTheme theme = GetNode<MainPlatformingTheme>("/root/MainPlatformingThemeStream");

		if(!theme.Playing)
			theme.Play();
	}

}
