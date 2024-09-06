using Godot;

public partial class FlyBehavior : Area2D
{
	[Export] public Sprite2D Sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Match level count to fly assets used
		FlyCount.TotalLevelFlies++;
	}

	// Letting the player eat the fly
	private void Collect(Node2D node)
	{
		if (node is Player || node is TongueProjectile || node.GetParent() is TongueLine)
		{
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
