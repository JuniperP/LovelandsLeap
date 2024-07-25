using Godot;
public partial class MonitorStandIn : Control
{
	// The number displayed on the screen of the monitor symbol
	public int number;

	// The chosen monitor to use
	public static int chosenScreen = 0;

	// Reference to the last focused node
	public static MonitorStandIn lastMainDisplay;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Setting the number of the monitor
		GetNode<TextureRect>("MonitorLogo").GetNode<Label>("GivenNumber").Text = $"{number + 1}";
	}

	// Adjusting for if the monitor is selected
	private void OnSelect()
	{
		if (chosenScreen != number)
		{
			// Adjusting the chosen screen
			chosenScreen = number;

			// Changing the screen displayed to
			DisplayServer.WindowSetCurrentScreen(number, GetWindow().GetWindowId());

			// Changing the main displays around
			lastMainDisplay.Modulate = new Color(1, 1, 1, 0.5f);
			Modulate = new Color(1, 1, 1, 1f);
			lastMainDisplay = this;
		}

	}
}
