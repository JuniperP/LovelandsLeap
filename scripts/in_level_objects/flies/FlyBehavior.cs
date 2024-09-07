using Godot;

public partial class FlyBehavior : Area2D
{
	[Export] public Sprite2D Sprite;
	private bool _noDoubleDown;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Match level count to fly assets used
		FlyCount.TotalLevelFlies++;

		_noDoubleDown = true;
	}

	// Letting the player eat the fly
	private void Collect(Node2D node)
	{
		if ((node is Player || node is TongueProjectile || node.GetParent() is TongueLine) && _noDoubleDown)
		{
			_noDoubleDown = false;
			SoundManager.PlaySound(SFX.FlyCatch, GetTree().Root);
			FlyCount.UpCount();
			QueueFree();
		}
	}

	// Toggle the way the fly is looking
	public void FlipFly()
	{
		Sprite.FlipH = !Sprite.FlipH;
	}
}
