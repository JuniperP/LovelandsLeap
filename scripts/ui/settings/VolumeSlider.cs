using Godot;
using System;

public partial class VolumeSlider : HSlider
{
	// Getting the bus to change 
	[Export] private String BusName;
	private int BusIndex;


	// Our number following the slider
	private Label OurNum;
 


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		

		// Setting our slider to adjusts to work with the provided bus
		BusIndex = AudioServer.GetBusIndex(BusName);

		// Sets sliders current value to match the bus
		Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(BusIndex));
	}

	// Setting our new volume
	protected void SetVolume(float newVol)
	{
		AudioServer.SetBusVolumeDb(BusIndex, Mathf.LinearToDb(newVol));
	}


	// Updates the percentage at the end of the slider
	protected void ChangePercent(float newNum)
	{
		// Sets up our percentage and default value
		OurNum = (Label)GetNode("Percentage");
		String numToAdd = "" + Mathf.Round(newNum * 100);
		OurNum.Text = numToAdd + "%";

	}

}
