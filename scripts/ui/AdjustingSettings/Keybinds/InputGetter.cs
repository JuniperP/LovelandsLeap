using Godot;
using System;
//using System.Net.Mail;

public partial class InputGetter : Toggleable
{
    public override void _Ready()
    {
        _close();
    }

    public override void _Input(InputEvent OurInput)
    {
        if (OurInput is InputEventMouseButton || OurInput is InputEventKey )
        {
            ChangeValue(OurInput);
            _close();
        }
       
    }

    protected virtual void ChangeValue(InputEvent OurInput){;}
}
