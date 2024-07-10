using Godot;
using System;

public partial class VolumeManager : Label
{
	// Getting the bus to change 
	[Export] private String BusName;
	private int BusIndex;

	// The slider we will be adjusting
	private HSlider Slider;

	// Our percentage following the slider
	private Label OurNum;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Getting our slider to control
		Slider = (HSlider)GetNode("VolumeSlider");

		// Changing the label of our combo accordingly
		Text = $"{BusName} Volume";

		// Setting our slider to adjusts to work with the provided bus
		BusIndex = AudioServer.GetBusIndex(BusName);
	}

	// Updating the current info
	private void _update()
	{
		// Sets sliders current value to match the bus
		if(Visible)
			Slider.Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(BusIndex));

	}


	// Setting our new volume while also updating the percentage
	protected void SetVolume(float NewVol)
	{
		// Updates volume 
		AudioServer.SetBusVolumeDb(BusIndex, Mathf.LinearToDb(NewVol));

		// Sets up our percentage
		Label PerLabel = (Label)Slider.GetNode("Percentage");
		String numToAdd = $"{Mathf.Round(NewVol * 100)}";
		PerLabel.Text = $"{numToAdd}%";
	}

}
