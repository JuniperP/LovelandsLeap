public partial class ConfirmReset : Toggleable
{
	// Used to reset the default settings
	private void ResetSettings()
	{
		LoadSettingsData.LoadData(true);
		Close();
	}
}
