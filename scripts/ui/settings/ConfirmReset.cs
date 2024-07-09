using Godot;
using System;

public partial class ConfirmReset : Toggleable
{	
	// Used to reset the default settings
	private void _reset_settings()
	{
		LoadSettingsData.LoadData(true);
		_close();
	}
}
