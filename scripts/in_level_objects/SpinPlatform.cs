using Godot;

public partial class SpinPlatform : AnimatableBody2D
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Rotate((float)-delta);
	}
}
