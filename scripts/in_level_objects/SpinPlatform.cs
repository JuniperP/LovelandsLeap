using Godot;

public partial class SpinPlatform : AnimatableBody2D
{
	[Export] public float Speed = -1;
	[Export] public bool test = false;

	// Don't ask me why this is needed 
	private float _middleMan = 0f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_middleMan += (float)delta * Speed;
		Rotation = _middleMan;
	}
}
