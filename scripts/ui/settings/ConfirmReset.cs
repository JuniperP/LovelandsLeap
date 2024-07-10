using Godot;

public partial class ConfirmReset : Toggleable
{

	// Boolean signifying if a reset occurred
	public static bool SettingsReset;

	// Used to reset the default settings
	private void _reset_settings()
	{
		LoadSettingsData.LoadData(true);
		SettingsReset = true;
		_close();
	}

	// public static void RefreshSettings(Node node)
	// {
	// 	// Getting the children to adjust
	// 	Settings set = (Settings)node.FindChild("Settings");
	// 	Button but = (Button)node.FindChild("SettingsButton");

	// 	// Set up for new settings
	// 	Settings set2 = (Settings)GD.Load<PackedScene>("res://scenes/ui/settings/settings.tscn").Instantiate();
	// 	set2.SetAnchorsPreset(Control.LayoutPreset.FullRect);
	// 	set2.Name = set.Name;

	// 	// Removing old child
	// 	set.QueueFree();

	// 	// Adding in the new settings
	// 	node.AddChild(set2);
	// 	GD.Print(node.GetChildren());
	// 	but.Pressed += () => set2.Visible = true;

	// 	// Completed goal of resetting
	// 	SettingsReset = false;
	// }
}
