using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public int Speed = 150;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		handleMovement(delta);
		base._PhysicsProcess(delta);
	}

	private void handleMovement(double delta)
	{
		Vector2 velocity = Vector2.Zero;
		
		if (Input.IsActionPressed("move_right"))
			velocity.X += 1;
		
		if (Input.IsActionPressed("move_left"))
			velocity.X -= 1;
		
		velocity = velocity.Normalized() * Speed;
		Position += velocity * (float)delta;
	}
}
