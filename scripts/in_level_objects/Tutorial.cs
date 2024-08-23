using Godot;


public partial class Tutorial : Node
{
	// Each of the text to alter
	[Export] private Label _walk;
	[Export] private Label _jump;
	[Export] private Label _holdJump;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Assigning all of the text using their keybinds
		_walk.Text = $"Walk using [{Keybinds._acts[UserAction.Left].Input.AsText()}] \n and [{Keybinds._acts[UserAction.Right].Input.AsText()}]";
		_jump.Text = $"Jump using [{Keybinds._acts[UserAction.Jump].Input.AsText()}]";
		_holdJump.Text = $"Hold [{Keybinds._acts[UserAction.Jump].Input.AsText()}] to\n jump higher";

		// Playing the main theme
		MainPlatformingTheme theme = GetNode<MainPlatformingTheme>("/root/MainPlatformingThemeStream");
		if(!theme.Playing)
			theme.Play();
	}
}
