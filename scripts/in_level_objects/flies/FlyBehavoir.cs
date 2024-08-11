using Godot;

public partial class FlyBehavoir : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Match level count to fly assets used
		FlyCount.TotalLevelFlies++;
	}

	// Letting the player eat the fly
	private void Collect(Node2D node)
	{
		if (node is Player || node is TongueProjectile)
		{
			SoundManager.PlaySound(SFX.Croak, GetTree().Root);
			FlyCount.UpCount();
			QueueFree();
		}
	}
}
