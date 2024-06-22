using Godot;
using System;

public partial class VolumeSlider : HSlider
{
	// Getting the bus to change 
	protected int BusIndex;

	// Setting our new volume
	protected void _set_volume(float newVol)
	{
		AudioServer.SetBusVolumeDb(BusIndex, Mathf.LinearToDb(newVol));
	}

	// Set slider based on if it's the first time the game was run
	protected void Setup(int BusIndex)
	{
		// Sets sliders current value to match the bus
		Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(BusIndex)); 
		
	}
}
