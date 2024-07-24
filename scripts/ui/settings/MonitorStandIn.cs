using Godot;
public partial class MonitorStandIn : Control
{
	// The number displayed on the screen of the monitor symbol
	[Export] public int number;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Setting the number of the monitor
		GetNode<TextureRect>("MonitorLogo").GetNode<Label>("GivenNumber").Text = $"{number}";
	}
}
