using Godot;
using System;

public abstract partial class MultiAnimation2D : Node2D, ICutAnimatable
{
	[Export] public AnimatedSprite2D Sprite;
	[Export] public double[] LifeSpans = { 1d };
	[Export] public double[] NextTriggers = { 1d };
	[Export] public string[] SpriteAnims = { "default" };

	protected int _animationCount;
	protected int _stage;
	private Action<double>[] _animations;

	public override void _Ready()
	{
		_animations = SetupAnimations();

		_animationCount = _animations.Length;
		if (LifeSpans.Length < _animationCount || NextTriggers.Length < _animationCount)
			throw new InvalidOperationException(
				$"LifeSpans or NextTriggers missing element(s) (expected {_animationCount})."
			);

		// Only work with sprite if sprite isn't null
		if (Sprite is null)
			return;

		string[] newAnims = new string[_animationCount];
		SpriteAnims.CopyTo(newAnims, 0);

		for (int i = 0; i < newAnims.Length; i++)
			if (newAnims[i] is null || newAnims[i].Equals(""))
				newAnims[i] = "default";

		SpriteAnims = newAnims;
	}

	protected abstract Action<double>[] SetupAnimations();

	public double TriggerAnimation()
	{
		if (_stage > _animationCount)
			throw new InvalidOperationException(
				"No remaining animations to trigger."
			);

		_animations[_stage](LifeSpans[_stage]);

		if (Sprite is not null)
		{
			Sprite.Animation = SpriteAnims[_stage];
			Sprite.Play();
		}

		return NextTriggers[_stage++];
	}

	protected void SetTimer(double timeSec, Action action)
	{
		SceneTreeTimer timer = GetTree().CreateTimer(timeSec);
		timer.Timeout += action;
	}
}
