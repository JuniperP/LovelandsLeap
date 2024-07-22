public partial class ToggleSpeedrun : ToggleButton
{
    public static bool HaveTimer = false;

    public override void Toggle()
    {
        // TODO:
        // used just as a signal ?
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
