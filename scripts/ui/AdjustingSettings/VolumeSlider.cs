using Godot;
using System;

public partial class VolumeSlider : HSlider
{
	// Getting the bus to change 
	protected int BusIndex;

	// Our number following the slider
	protected Label OurNum;

	// Setting our new volume
	protected void _set_volume(float newVol)
	{
		AudioServer.SetBusVolumeDb(BusIndex, Mathf.LinearToDb(newVol));
	}


	// Set slider based on if it's the first time the game was run
	protected void _setup(int BusIndex)
	{
		// Sets sliders current value to match the bus
		Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(BusIndex));

	

	}


	/* 
	 Updates the number_percent at the end of the slider
	 (Note slider should be turned into a scene with slider so needed relation of slider and % is more obvious)
	*/
	protected void _change_percent(float newNum)
	{
		// Sets up our percentage and default value
		OurNum = (Label)GetNode("Percentage");
		String numToAdd = "" + Mathf.Round(newNum * 100);
		OurNum.Text = numToAdd + "%";

	}

}
