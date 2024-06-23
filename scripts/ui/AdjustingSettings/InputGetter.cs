using Godot;
using System;
//using System.Net.Mail;

public partial class InputGetter : Toggleable
{
    public override void _Ready()
    {
        _open();
    }

    public override void _Input(InputEvent ourInput)
    {
        if (ourInput is InputEventMouseButton || ourInput is InputEventKey )
        {
            ChangeValue(ourInput);
            _close();
        }
       
    }

    protected virtual void ChangeValue(InputEvent ourInput){;}
}
