public partial class ToggleSpeedrun : ToggleButton
{
    public static bool HaveTimer = false;

    public override void Toggle()
    {
        if(!GetState())
            SpeedRunTimer.ResetRun();
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
