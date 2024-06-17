using Godot;
using System;

public partial class Credits : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_up"))
		{
			GetTree().ChangeSceneToFile("res://scenes/mainMenu.tscn");
		}
	}
}
