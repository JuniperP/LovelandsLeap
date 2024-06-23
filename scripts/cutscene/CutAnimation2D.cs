using Godot;

public partial class CutAnimation2D : Node2D, ICutAnimatable
{
	[Export] public double LifeSpan = 1d;
	[Export] public double NextTrigger = 1d;

	public double TriggerAnimation()
	{
		Visible = true;

		SceneTreeTimer timer = GetTree().CreateTimer(LifeSpan);
		timer.Timeout += () => Visible = false;

		return NextTrigger;
	}
}
