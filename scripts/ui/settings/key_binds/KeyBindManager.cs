using Godot;
using System;

public partial class KeyBindManager : Control
{
	// Choice for which button we will be using this combo to set
	[Export] private UserAction ActionToSet = UserAction.Left;

	// The button containing the name of the current key bind
	private Button OurButton;

	// Used to see if a new key bind is about to be set
	private Boolean ToBeSet;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Getting the children we will need
		Label label = (Label)GetNode("MoveLabel");
		OurButton = (Button)label.GetNode("ButtonToAdjust");

		// Giving a special name for each button's accompanying label
		label.Text = Keybinds._acts[ActionToSet].ButtonLabel;

		// Set up changing the value in the future
		ToBeSet = false;
	}

	// Updating the current info
	private void _update()
	{
		// Giving the symbol for each button
		if(Visible)
			OurButton.Text = Keybinds._acts[ActionToSet].Input.AsText();
	}


	// Used to adjust the key bind if user asks for a change
	private void ChangeValue()
	{
		OurButton.Text = "...";
		ToBeSet = true;

		// Stopping the instance of escaping right after entering a key bind
		if(ActionToSet == UserAction.Cancel)
			InputMap.ActionEraseEvents(Keybinds._acts[ActionToSet].Mapping);
	}


	// Checks for any input and if a valid input is given it is sent to change our key binds
	public override void _Input(InputEvent OurInput)
	{
		// Makes sure input is looked for and valid
		if ((OurInput is InputEventMouseButton || OurInput is InputEventKey) && ToBeSet)
		{
			// Sets the key bind
			KeyBindSetterHelper.SetKeyBind(OurInput, ActionToSet);

			// Updating our button's displayed symbol
			OurButton.Text = OurInput.AsText();

			// Resets marker of to be set
			ToBeSet = false;
		}

	}

}
