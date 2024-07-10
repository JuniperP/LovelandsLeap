using Godot;
using System;

public partial class ToggleSpeedrun : ToggleButton
{
    public static bool HaveTimer = false;

    public override void Toggle()
    {
        // To be implemented
    }

    protected override bool GetState()
    {
        return HaveTimer;
    }

    protected override void SetState(bool state)
    {
        HaveTimer = state;
    }
}
