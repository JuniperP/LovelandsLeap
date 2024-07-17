using Godot;

public partial class Arrow : Toggleable
{

    private void InArea(Node2D node)
	{
		if(node is Player)
			Open();
	}

	private void OutArea(Node2D node)
	{
		if(node is Player)
			Close();
	}
}
