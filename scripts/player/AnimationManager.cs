using Godot;

public class AnimationManager
{
	public enum State
	{
		Idle,
		Walking,
		Jump,
		Grapple
	}

	private State _animState = State.Idle;
	public State AnimState
	{
		get { return _animState; }
		set { SetState(value); }
	}

	public bool FlipH
	{
		get { return _sprite.FlipH; }
		set { _sprite.FlipH = value; }
	}

	private readonly Player _ctx;
	private readonly AnimatedSprite2D _sprite;

	public AnimationManager(Player player)
	{
		_ctx = player;
		_sprite = _ctx.GetNode<AnimatedSprite2D>("Sprite");
	}

	private void SetState(State state)
	{
		if (state == _animState)
			return;
		_animState = state;

		// TODO: Implement switch statement for animations
	}
}
