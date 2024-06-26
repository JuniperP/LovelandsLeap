using Godot;
using System;
//using System.Net.Mail;

public partial class InputGetter : Toggleable
{
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
    protected virtual void ChangeValue(InputEvent OurInput) {; }
}
