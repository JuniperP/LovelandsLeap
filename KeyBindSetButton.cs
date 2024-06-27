using Godot;
using System;

public partial class KeyBindSetButton : Control
{
	// Choice for which button we will be using this combo to set
	[Export] private Boolean LeftButton;
	[Export] private Boolean RightButton;
	[Export] private Boolean JumpButton;
	[Export] private Boolean TongueButton;




	// The button containing the name of the current key bind
	private Button OurButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Getting the children we will need
		SetKeyBinds toSet = (SetKeyBinds)GetNode("SetKeyBinds");
		Label label = (Label)GetNode("MoveLabel");
		OurButton = (Button)label.GetNode("ButtonToAdjust");

		// Setting the action and name besides the button accordingly
		if (LeftButton)
		{
			label.Text = "Move Left";
			toSet.Mapping = "move_left";
		}
		else if (RightButton)
		{
			label.Text = "Move Right";
			toSet.Mapping = "move_right";
		}
		else if (JumpButton)
		{
			label.Text = "Jump";
			toSet.Mapping = "move_up";
		}
		else if (TongueButton)
		{
			label.Text = "Use Tongue";
			toSet.Mapping = "primary_click";
		}
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Setting the label of the button accordingly
		if (LeftButton) { OurButton.Text = Keybinds.LeftSym; }
		else if (RightButton) { OurButton.Text = Keybinds.RightSym; }
		else if (JumpButton) { OurButton.Text = Keybinds.JumpSym; }
		else if (TongueButton) { OurButton.Text = Keybinds.ClickSym; }
	}
}
