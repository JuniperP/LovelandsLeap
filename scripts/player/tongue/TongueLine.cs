using Godot;

public partial class TongueLine : Line2D
{
	[Export] public Node2D Target;

	[Export] private CollisionShape2D _hitBox;

	public override void _Process(double delta)
	{
		// Set second line point position to the target's relative position
		SetPointPosition(1, Target.GlobalPosition - GlobalPosition);

		// Matching the hit box to the line
		((SegmentShape2D)_hitBox.Shape).B = Target.GlobalPosition - GlobalPosition;
	}
}
