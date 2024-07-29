using Godot;

public partial class ConfirmReset : Toggleable
{
	// Used to reset the default settings
	private void ResetSettings()
	{
		// Loading in the new data
		LoadSettingsData.LoadData(true);

		// Adjusting screen accordingly
		if (MonitorStandIn.ChosenScreen >= DisplayServer.GetScreenCount())
			MonitorStandIn.TempScreenChoice = GetWindow().CurrentScreen;
		else
			DisplayServer.WindowSetCurrentScreen(MonitorStandIn.ChosenScreen, GetWindow().GetWindowId());

		// Closing the screen
		Close();
	}
}
