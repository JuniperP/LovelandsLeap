using Godot;
public partial class MonitorStandIn : Control
{
	// The number displayed on the screen of the monitor symbol
	public int number;

	// The chosen primary monitor to use
	public static int chosenScreen = 0;

	// The filler choice if you don't have your primary monitor connected
	public static int tempChoice = -1;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Setting the number of the monitor
		GetNode<TextureRect>("MonitorLogo").GetNode<Label>("GivenNumber").Text = $"{number + 1}";

		// Modulating to match set up as needed
		if (DisplayChanging.currentScreen == number)
			Modulate = new Color(1, 1, 1, 1f);
		else
			Modulate = new Color(1, 1, 1, 0.5f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Checking only at a change when the user wants
		if (tempChoice == -1 && DisplayChanging.currentScreen != chosenScreen)
		{
			// Setting symbols appropriately
			if (DisplayChanging.currentScreen == number)
				Modulate = new Color(1, 1, 1, 1f);
			else
				Modulate = new Color(1, 1, 1, 0.5f);

			// Setting the new choice at the final run through (Godot goes in linear order)
			if(number + 1 == DisplayServer.GetScreenCount())
				chosenScreen = DisplayChanging.currentScreen;

		}

	}

	// Adjusting for if the monitor is selected
	private void OnSelect()
	{
		if (chosenScreen != number)
		{
			tempChoice = -1;
			DisplayServer.WindowSetCurrentScreen(number, GetWindow().GetWindowId());
		}

	}
}
