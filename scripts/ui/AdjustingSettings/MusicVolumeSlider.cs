using Godot;
using System;

public partial class MusicVolumeSlider : VolumeSlider
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Getting setting our bus to adjusts to be the Master bus
		BusIndex = AudioServer.GetBusIndex("Music");

		// Sets up slider
		_setup(BusIndex);
	}

}
