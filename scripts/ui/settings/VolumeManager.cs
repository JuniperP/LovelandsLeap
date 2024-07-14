using Godot;
using System;

public partial class VolumeManager : Label
{
	// Getting the bus to change 
	[Export] private string _busName;
	private int _busIndex;

	// The slider we will be adjusting
	private HSlider _slider;

	// Our percentage following the slider
	private Label _ourNum;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Getting our slider to control
		_slider = (HSlider)GetNode("VolumeSlider");

		// Changing the label of our combo accordingly
		Text = $"{_busName} Volume";

		// Setting our slider to adjusts to work with the provided bus
		_busIndex = AudioServer.GetBusIndex(_busName);
	}

	// Updating the current info
	private void Update()
	{
		// Sets sliders current value to match the bus
		if(Visible)
			_slider.Value = Mathf.DbToLinear(AudioServer.GetBusVolumeDb(_busIndex));

	}


	// Setting our new volume while also updating the percentage
	protected void SetVolume(float newVol)
	{
		// Updates volume 
		AudioServer.SetBusVolumeDb(_busIndex, Mathf.LinearToDb(newVol));

		// Sets up our percentage
		Label perLabel = (Label)_slider.GetNode("Percentage");
		string numToAdd = $"{Mathf.Round(newVol * 100)}";
		perLabel.Text = $"{numToAdd}%";
	}

}
