using Godot;
using System;

public partial class KeyBindManager : Control
{
	// Choice for which button we will be using this combo to set
	[Export] private Boolean LeftButton;
	[Export] private Boolean RightButton;
	[Export] private Boolean JumpButton;
	[Export] private Boolean DownButton;

	[Export] private Boolean TongueButton;
	[Export] private Boolean CancelButton;


	// The button containing the name of the current key bind
	private Button OurButton;

	// Used to see if a new key bind is about to be set
	private Boolean ToBeSet;

	// Used to see what our button will be mapping to
	private String Mapping;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Getting the children we will need
		Label label = (Label)GetNode("MoveLabel");
		OurButton = (Button)label.GetNode("ButtonToAdjust");

		// Setting the action and name besides the button accordingly
		if (LeftButton)
		{
			OurButton.Text = Keybinds.LeftSym;
			label.Text = "Move Left";
			Mapping = "move_left";
		}
		else if (RightButton)
		{
			OurButton.Text = Keybinds.RightSym;
			label.Text = "Move Right";
			Mapping = "move_right";
		}
		else if (JumpButton)
		{
			OurButton.Text = Keybinds.JumpSym;
			label.Text = "Jump";
			Mapping = "move_up";
		}
		else if (DownButton)
		{
			OurButton.Text = Keybinds.DownSym;
			label.Text = "Fast Fall";
			Mapping = "move_down";
		}
		else if (TongueButton)
		{
			OurButton.Text = Keybinds.ClickSym;
			label.Text = "Use Tongue";
			Mapping = "primary_click";
		}
		else
		{
			OurButton.Text = Keybinds.CancelSym;
			label.Text = "Cancel";
			Mapping = "ui_cancel";
		}


		// Set up changing the value in the future
		ToBeSet = false;
	}

	// Used to adjust the key bind if user asks for a change
	private void ChangeValue()
	{
		OurButton.Text = "...";
		ToBeSet = true;
	}

	// Checks for any input and if a valid input is given it is sent to change our key binds
	public override void _Input(InputEvent OurInput)
	{
		// Makes sure input is looked for and valid
		if ((OurInput is InputEventMouseButton || OurInput is InputEventKey) && ToBeSet)
		{

			// Gets rid of all other key binds
			InputMap.ActionEraseEvents(Mapping);


			// Updates current mapped button label and the stored action
			if (LeftButton)
			{
				Keybinds.LeftSym = OurInput.AsText();
				Keybinds.LeftIn = OurInput;
			}
			else if (RightButton)
			{
				Keybinds.RightSym = OurInput.AsText();
				Keybinds.RightIn = OurInput;
			}
			else if (JumpButton)
			{
				Keybinds.JumpSym = OurInput.AsText();
				Keybinds.JumpIn = OurInput;
			}
			else if (DownButton)
			{
				Keybinds.DownSym = OurInput.AsText();
				Keybinds.DownIn = OurInput;
			}
			else if (TongueButton)
			{
				Keybinds.ClickSym = OurInput.AsText();
				Keybinds.ClickIn = OurInput;
			}
			else
			{
				Keybinds.CancelSym = OurInput.AsText();
				Keybinds.CancelIn = OurInput;
			}


			// Maps wanted button
			InputMap.ActionAddEvent(Mapping, OurInput);


			// Updating our button
			OurButton.Text = OurInput.AsText();


			// Resets marker of to be set
			ToBeSet = false;
		}

	}

}
