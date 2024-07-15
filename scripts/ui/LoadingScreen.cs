using Godot;
public partial class LoadingScreen : Toggleable
{
	// A count of how many dots to print
	private double _count;

	// Our label we are adjusting
	private Label _ourLabel;

	// the new text to be assigned to our label
	private string newText;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Close();
		_count = 0;
		_ourLabel = GetNode<Label>("TemporaryLoadingScreen");
		newText = "";
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Visible)
		{
			// Setting counts new value
			_count = (_count + delta * 2) % 4;

			// The new text to be assigned to our label
			newText = "Loading";

			// Adding the loading ellipses
			for (int i = 1; i < _count; i++)
			{
				newText += ".";
			}

			// Assigning the new text
			_ourLabel.Text = newText;
		}
	}
}
