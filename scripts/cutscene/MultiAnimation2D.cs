using Godot;
using System;

public abstract partial class MultiAnimation2D : Node2D, ICutAnimatable
{
	[Export] public double[] LifeSpans = { 1d };
	[Export] public double[] NextTriggers = { 1d };

	protected int _animationCount;
	private int _stage;
	private Action<double>[] _animations;

	public override void _Ready()
	{
		_animations = SetupAnimations();

		_animationCount = _animations.Length;
		if (LifeSpans.Length < _animationCount || NextTriggers.Length < _animationCount)
			throw new InvalidOperationException(
				$"LifeSpans or NextTriggers missing element(s) (expected {_animationCount})."
			);
	}

	protected abstract Action<double>[] SetupAnimations();

	public double TriggerAnimation()
	{
		if (_stage > _animationCount)
			throw new InvalidOperationException(
				"No remaining animations to trigger."
			);

		_animations[_stage](LifeSpans[_stage]);

		return NextTriggers[_stage++];
	}

	protected void SetTimer(double timeSec, Action action)
	{
		SceneTreeTimer timer = GetTree().CreateTimer(timeSec);
		timer.Timeout += action;
	}
}
