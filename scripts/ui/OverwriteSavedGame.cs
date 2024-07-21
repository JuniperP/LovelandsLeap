using Godot;

public partial class OverwriteSavedGame : Toggleable
{
	// Signal to say we confirmed a reset
	[Signal] public delegate void OverwriteEventHandler();

	private void OverwriteSave()
	{
		Close();
		EmitSignal(SignalName.Overwrite);
	}
}
