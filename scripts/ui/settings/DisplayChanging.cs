using Godot;

public partial class DisplayChanging : Label
{
	// The current displayed number of screens
	private int _numOfDis;

	// The current screen being displayed to
	public static int CurrentScreen;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_numOfDis = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Updating the current screen
		CurrentScreen = GetWindow().CurrentScreen;

		// Ensuring that windows aren't messed with for no reason
		if (CurrentScreen != MonitorStandIn.TempScreenChoice)
		{
			MonitorStandIn.TempScreenChoice = -1;
		}

		// Ensuring the number of displays matches displays to select
		if (Visible && _numOfDis != DisplayServer.GetScreenCount())
		{
			// Designated index
			int index;

			// Removing children
			Node node;
			for (index = GetChildCount(); index > 0; index--)
			{
				node = GetChild(index - 1);
				RemoveChild(node);
				node.QueueFree();
			}

			// How much to reposition by
			int repos = 0;

			// Adding new children
			MonitorStandIn newMon;
			for (index = 0; index < DisplayServer.GetScreenCount(); index++)
			{
				// Hard limit on the amount of screens we can support
				if (index >= 4)
					break;

				// New monitor
				newMon = (MonitorStandIn)GD.Load<PackedScene>(SceneManager.GetPath(ToScene.Monitor)).Instantiate();

				// Sets the number
				newMon.MonitorNum = index;

				// Adds as child
				AddChild(newMon);

				// Anchors child
				newMon.SetAnchor(Side.Bottom, 1);

				// Adjusts position
				newMon.SetPosition(new Vector2(repos, 75));
				repos += 215;

				// Adjusting the size
				newMon.SetSize(new Vector2(200, 150));
			}

			// Updating count
			_numOfDis = DisplayServer.GetScreenCount();
		}
	}
}
