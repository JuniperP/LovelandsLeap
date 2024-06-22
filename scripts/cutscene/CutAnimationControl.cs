using Godot;

public partial class CutAnimationControl : Control, ICutAnimatable
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
