using Godot;
using System;
using System.Collections.Generic;

public enum AnimState : byte
{
	Idle,
	Walking,
	Jumping,
	Tongue,
}

public class AnimationManager
{
	private readonly Dictionary<AnimState, string> _animNames = new()
	{
		{AnimState.Idle, "idle"},
		{AnimState.Walking, "walking"},
		{AnimState.Jumping, "jumping"},
		{AnimState.Tongue, "tongue"},
	};

	private AnimState _animState = AnimState.Idle;
	public AnimState State
	{
		get { return _animState; }
		set { SetState(value); }
	}

	public bool IsLeftFacing
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

	private void SetState(AnimState state)
	{
		if (state == _animState)
			return;
		_animState = state;

		_sprite.Play(_animNames[state]);
	}
}
