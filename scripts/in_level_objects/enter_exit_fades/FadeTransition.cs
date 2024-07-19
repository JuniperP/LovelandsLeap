using Godot;

public partial class FadeTransition : Area2D
{
	// Where the player is going to
	public ToScene sendsTo;

	// What is signaled when the player enters - sends to new scene
	private void FrogEntered(Node2D node)
	{
		if (node is Player)
		{
			SceneManager.SetNextGoTo(sendsTo);
			GetTree().Paused = true;
			LoadingScreen.FadeIn();
		}

	}


}
