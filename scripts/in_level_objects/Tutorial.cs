using Godot;


public partial class Tutorial : Node
{
	// Each of the text to alter
	[Export] private Label _walk;
	[Export] private Label _jump;
	[Export] private Label _holdJump;
	[Export] private Label _useTongue;
	[Export] private Label _jumpAndHold1;
	[Export] private Label _jumpAndHold2;
	[Export] private Label _swing;
	[Export] private Label _pause;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Assigning all of the text using their keybinds
		_walk.Text = $"Walk using [{Keybinds._acts[UserAction.Left].Input.AsText()}] \nand [{Keybinds._acts[UserAction.Right].Input.AsText()}]";
		_jump.Text = $"Jump using [{Keybinds._acts[UserAction.Jump].Input.AsText()}]";
		_holdJump.Text = $"Hold [{Keybinds._acts[UserAction.Jump].Input.AsText()}] to\njump higher";
		_useTongue.Text = $"Point your mouse and\npress [{Keybinds._acts[UserAction.PAction].Input.AsText()}]\nto use your tongue\n and hit the button";
		_jumpAndHold1.Text = $"Jump and hold onto the\nplatform with [{Keybinds._acts[UserAction.Jump].Input.AsText()}] &\n[{Keybinds._acts[UserAction.PAction].Input.AsText()}]";
		_jumpAndHold2.Text = $"Disengage with\n[{Keybinds._acts[UserAction.SAction].Input.AsText()}]";
		_swing.Text = $"Swing [{Keybinds._acts[UserAction.Left].Input.AsText()}] and [{Keybinds._acts[UserAction.Right].Input.AsText()}]\nto swing yourself\nwhile grappled";
		_pause.Text = $"Press [{Keybinds._acts[UserAction.Cancel].Input.AsText()}]\nat any time to\npause the game";

		// Playing the main theme
		MainPlatformingTheme theme = GetNode<MainPlatformingTheme>("/root/MainPlatformingThemeStream");
		if(!theme.Playing)
			theme.Play();
	}

	// Easy signal tells to change the button label accordingly
	private void DisengageTongue()
	{
		_useTongue.Text = $"Disengage your tongue by\npressing [{Keybinds._acts[UserAction.SAction].Input.AsText()}]\nor touching a surface";
	}
}
