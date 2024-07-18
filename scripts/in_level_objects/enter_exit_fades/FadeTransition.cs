using Godot;

public partial class FadeTransition : Area2D
{
	public ToScene sendsTo;

	private void FrogEntered(Node2D node)
	{
		if(node is Player)
			SceneManager.SetNextGoTo(sendsTo);
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		SceneManager.GoToSetScene(this);
	}


}
