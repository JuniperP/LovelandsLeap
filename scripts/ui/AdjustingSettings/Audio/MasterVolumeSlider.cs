using Godot;
using System;

public partial class MasterVolumeSlider : VolumeSlider
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Getting setting our bus to adjusts to be the Master bus
		BusIndex = AudioServer.GetBusIndex("Master");

		// Sets up slider
		Setup(BusIndex);

	}

}
