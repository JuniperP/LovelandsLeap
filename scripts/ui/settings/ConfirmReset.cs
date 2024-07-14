using Godot;

public partial class ConfirmReset : Toggleable
{

	// Boolean signifying if a reset occurred
	public static bool SettingsReset;

	// Used to reset the default settings
	private void ResetSettings()
	{
		LoadSettingsData.LoadData(true);
		SettingsReset = true;
		Close();
	}
}
