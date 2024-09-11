using Godot;

public partial class InLevelButton : Area2D
{
	// Signals to notifying the rest of scene of button changes
	[Signal] public delegate void ButtonPressedEventHandler();
	[Signal] public delegate void ButtonReleasedEventHandler();

	private bool _pressed;
	// Indicating whether buttons stay pushed down or pop back up
	[Export] public bool StaysOn;

	[ExportGroup("Sprites & Sounds")]
	[Export] public Sprite2D OnStateSprite;
	[Export] public Sprite2D OffStateSprite;
	[Export] public AudioStreamPlayer Sfx;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_pressed = false;
	}

	// Easy signals for button press/release functionality
	private void PressButton(Node2D node)
	{
		if (!_pressed && (node is Player || node is TongueProjectile))
		{
			ToggleButton();
			Sfx.Play();
			EmitSignal(SignalName.ButtonPressed);
		}
	}

	private void ReleaseButton(Node2D node)
	{
		if (_pressed && !StaysOn && (node is Player || node is TongueProjectile))
		{
			ToggleButton();
			EmitSignal(SignalName.ButtonReleased);
		}
	}


	// Helper function for toggling button state
	private void ToggleButton()
	{
		_pressed = !_pressed;

		// Change sprite state to match click state
		if (OnStateSprite.Visible == true)
		{
			OnStateSprite.Hide();
			OffStateSprite.Show();
		}
		else
		{
			OnStateSprite.Show();
			OffStateSprite.Hide();
		}

	}

}
