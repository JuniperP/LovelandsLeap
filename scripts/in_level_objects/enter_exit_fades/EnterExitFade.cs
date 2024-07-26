using Godot;
public partial class EnterExitFade : Node2D
{

	// Where we are sending the player to
	[Export] private ToScene _sendPlayerTo;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Sets the value of where we want the player to go
		GetNode<FadeTransition>("StartTransHitBox").sendsTo = _sendPlayerTo;
	}
}
