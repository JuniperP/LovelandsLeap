using Godot;
using System;

public partial class KeyBindSetButton : Control
{
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
		Label label = (Label)GetNode("MoveLabel");
		OurButton = (Button)label.GetNode("ButtonToAdjust");
		
		// Setting the name besides the button accordingly
		if (LeftButton) { label.Text = "Move Left"; }
		else if (RightButton) { label.Text = "Move Right";  }
		else if (JumpButton) { label.Text = "Jump";  }
		else if (TongueButton) { label.Text = "Use Tongue";  }
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
