using Godot;
using System;

public partial class VolumeSlider :  HSlider
{
	//Getting the bus to change 
	protected int BusIndex; 

	//Setting our new volume
	protected void _set_volume(float newVol)
	{
		AudioServer.SetBusVolumeDb(BusIndex, newVol);
	}
}
