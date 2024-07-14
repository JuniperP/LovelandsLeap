using Godot;
using System;

public partial class KeyBindManager : Control
{
	// Choice for which button we will be using this combo to set
	[Export] private UserAction _actionToSet = UserAction.Left;

	// The button containing the name of the current key bind
	private Button _ourButton;

	// Used to see if a new key bind is about to be set
	private bool _toBeSet;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Getting the children we will need
		Label label = (Label)GetNode("MoveLabel");
		_ourButton = (Button)label.GetNode("ButtonToAdjust");

		// Giving a special name for each button's accompanying label
		label.Text = Keybinds._acts[_actionToSet].ButtonLabel;

		// Set up changing the value in the future
		_toBeSet = false;
	}

	// Updating the current info
	private void UpdateText()
	{
		// Giving the symbol for each button
		if(Visible)
			_ourButton.Text = Keybinds._acts[_actionToSet].Input.AsText();
	}


	// Used to adjust the key bind if user asks for a change
	private void ChangeValue()
	{
		_ourButton.Text = "...";
		_toBeSet = true;

		// Stopping the instance of escaping right after entering a key bind
		if(_actionToSet == UserAction.Cancel)
			InputMap.ActionEraseEvents(Keybinds._acts[_actionToSet].Mapping);
	}


	// Checks for any input and if a valid input is given it is sent to change our key binds
	public override void _Input(InputEvent ourInput)
	{
		// Makes sure input is looked for and valid
		if ((ourInput is InputEventMouseButton || ourInput is InputEventKey) && _toBeSet)
		{
			// Sets the key bind
			KeyBindSetterHelper.SetKeyBind(ourInput, _actionToSet);

			// Updating our button's displayed symbol
			_ourButton.Text = ourInput.AsText();

			// Resets marker of to be set
			_toBeSet = false;
		}

	}

}
