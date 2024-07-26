using Godot;

public partial class Arrow : Toggleable
{
	// Displays arrow when player is in hit box and vice versa
	private void InArea(Node2D node)
	{
		if (node is Player)
			Open();
	}

	private void OutArea(Node2D node)
	{
		if (node is Player)
			Close();
	}
}
