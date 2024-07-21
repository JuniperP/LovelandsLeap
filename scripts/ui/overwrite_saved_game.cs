using Godot;

public partial class overwrite_saved_game : Toggleable
{
	// Signal to say we confirmed a reset
	[Signal] public delegate void OverwriteEventHandler();

	private void OverwriteSave()
	{
		Close();
		EmitSignal(SignalName.Overwrite);
	}
}
