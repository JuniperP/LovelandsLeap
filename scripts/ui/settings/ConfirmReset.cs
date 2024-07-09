using Godot;
using System;

public partial class ConfirmReset : Toggleable
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_close();
	}

	private void _reset_settings()
	{
		LoadSettingsData.LoadData(true);
		_close();
	}
}
