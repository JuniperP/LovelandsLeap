using Godot;
using System;

public partial class LoadIn : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SaveGame.LoadData();
		GetTree().ChangeSceneToFile("res://scenes/ui/main_menu.tscn");
	}


}
