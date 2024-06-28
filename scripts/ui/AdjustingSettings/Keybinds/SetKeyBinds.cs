using Godot;
using System;

public partial class SetKeyBinds : Toggleable
{
	// What we are mapping to (meant to be set by the parent of the key bind set combo)
	public String Mapping = "";

	// Close on loading in
	public override void _Ready()
	{
		_close();
	}

	// Checks for any input and if a valid input is given it is sent to change our key binds
	public override void _Input(InputEvent OurInput)
	{
		if (OurInput is InputEventMouseButton || OurInput is InputEventKey)
		{
			ChangeValue(OurInput);
			_close();
		}

	}

	// What values are precisely changed are to be based on the sub class
	protected void ChangeValue(InputEvent OurInput)
	{
		if (Visible)
		{


		}
	}
}
