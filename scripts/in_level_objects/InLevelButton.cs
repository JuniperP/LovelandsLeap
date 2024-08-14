using Godot;

public partial class InLevelButton : Area2D
{
	[Signal] public delegate void ButtonPressedEventHandler();

	[Export] public Sprite2D OnStateSprite;
	[Export] public Sprite2D OffStateSprite;

	private bool _pressed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_pressed = false;
	}

	// Easy signal button press functionality
	private void PressButton(Node2D node)
	{
		if (!_pressed && (node is Player || node is TongueProjectile))
		{
			_pressed = true;

			// Change sprite state to match click state
			OnStateSprite.Hide();
			OffStateSprite.Show();

			// Notifying a button press to rest of scene
			EmitSignal(SignalName.ButtonPressed);
		}

	}

}
