using Godot;

public partial class Settings : Toggleable
{
	// Setup by closing the visibility and prepping our held down check
	public override void _Ready()
	{
		Close();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Letting the user quit from settings
		if (Input.IsActionJustPressed("ui_cancel"))
			Close();
	}
}
