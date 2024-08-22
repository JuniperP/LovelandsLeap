using Godot;

public partial class InfoToSkip : Label
{
	public override void _Ready()
	{
		Text = $"Press [{Keybinds._acts[UserAction.Accept].Input.AsText()}] to continue\n";
		Text += $"Press [{Keybinds._acts[UserAction.Cancel].Input.AsText()}] to skip";
	}
}
