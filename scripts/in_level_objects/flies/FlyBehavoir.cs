using Godot;

public partial class FlyBehavoir : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Match level count to fly assets used
		FlyCount.TotalLevelFlies++;

		// Setting up right collision because godot is cringe
		SetCollisionLayerValue(2, true);
		SetCollisionMaskValue(2, true);

	}

	// Letting the player eat the fly
	private void Collect(Node2D node)
	{
		if (node is Player)
		{
			SoundManager.PlaySound(SFX.Croak, node);
			FlyCount.UpCount();
			QueueFree();
		}
	}
}
