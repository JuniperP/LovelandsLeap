using Godot;
public partial class MonitorStandIn : Control
{
	// The number displayed on the screen of the monitor symbol
	public int MonitorNum;

	// The chosen primary monitor to use
	public static int ChosenScreen = 0;

	// The filler choice if you don't have your primary monitor connected
	public static int TempScreenChoice = -1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Setting the number of the monitor
		GetNode<TextureRect>("MonitorLogo").GetNode<Label>("GivenNumber").Text = $"{MonitorNum + 1}";

		// Modulating to match set up as needed
		if (DisplayChanging.CurrentScreen == MonitorNum)
			Modulate = new Color(1, 1, 1, 1f);
		else
			Modulate = new Color(1, 1, 1, 0.5f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Checking only at a change when the user wants
		if (TempScreenChoice == -1 && DisplayChanging.CurrentScreen != ChosenScreen)
		{
			// Setting symbols appropriately
			if (DisplayChanging.CurrentScreen == MonitorNum)
				Modulate = new Color(1, 1, 1, 1f);
			else
				Modulate = new Color(1, 1, 1, 0.5f);

			// Setting the new choice at the final run through (Godot goes in linear order)
			if(MonitorNum + 1 == DisplayServer.GetScreenCount())
				ChosenScreen = DisplayChanging.CurrentScreen;
		}
	}

	// Adjusting for if the monitor is selected
	private void OnSelect()
	{
		if (ChosenScreen != MonitorNum)
		{
			TempScreenChoice = -1;
			DisplayServer.WindowSetCurrentScreen(MonitorNum, GetWindow().GetWindowId());
		}
	}
}
