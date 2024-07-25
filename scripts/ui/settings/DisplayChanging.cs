using Godot;

public partial class DisplayChanging : Label
{
	// The current displayed number of screens
	private int _numOfDis;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_numOfDis = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Ensuring the number of displays matches displays to select
		if (Visible && _numOfDis != DisplayServer.GetScreenCount())
		{
			// Designated index
			int i;

			// Removing children
			Node node;
			for (i = GetChildCount(); i > 0; i--)
			{
				node = GetChild(i - 1);
				RemoveChild(node);
				node.QueueFree();
			}

			// How much to reposition by
			int repo = 0;

			// Adding new children
			MonitorStandIn newMon;
			for (i = 0; i < DisplayServer.GetScreenCount(); i++)
			{
				// New monitor
				newMon = (MonitorStandIn)GD.Load<PackedScene>(SceneManager.GetPath(ToScene.Monitor)).Instantiate();

				// Sets the number
				newMon.number = i;

				// Adds as child
				AddChild(newMon);

				// Anchors child
				newMon.SetAnchor(Side.Bottom, 1);

				// Adjusts position
				newMon.SetPosition(new Vector2(repo, 75));
				repo += 215;

				// Adjusting the size
				newMon.SetSize(new Vector2(200, 150));
			}


			// Updating count
			_numOfDis = DisplayServer.GetScreenCount();
		}
	}
}
