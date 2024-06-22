using Godot;
using System;

public partial class number_percentage : Label
{
	// The number we want to display as a percentage
	public Label ourNumber;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Accessing our label
		ourNumber = (Label) this.GetNode("PNumber");

	}


}
