using Godot;

public partial class ToggleSpeedrun : ToggleButton
{
    // Whether or not the timer should be displayed
    public static bool HasTimer = false;

    // The players best speed run time (base = just above max)
    public static float PBTime = float.PositiveInfinity;

    // Adjusting and resetting speedruns
    public override void Toggle()
    {
        if (!GetState())
            SpeedRunTimer.ResetRun();
    }

    // Getters and setters
    protected override bool GetState()
    {
        return HasTimer;
    }

    protected override void SetState(bool state)
    {
        HasTimer = state;
    }

    // Allowing change to the pb time while also preventing none valid times
    public static void NewTime(float time)
    {
        if ((time < PBTime || PBTime <= 0) && time > 0)
            PBTime = time;
    }

    // Updating users presented pb
    private void UpdateTime()
    {
        // Getting the parent of the children we need
        Label current = GetNode<Label>("CurrentTimeLabel");

        // Each time to change
        Label centiSec = current.GetNode<Label>("Centiseconds");
        Label sec = current.GetNode<Label>("Seconds");
        Label min = current.GetNode<Label>("Minutes");

        // Accounting for no record
        if (PBTime == float.PositiveInfinity)
        {
            centiSec.Text = "na";
            sec.Text = "na";
            min.Text = "na";
        }
        else
        {
            // Displaying the users pb
            centiSec.Text = SpeedRunTimer.FormCentSec(PBTime);
            sec.Text = SpeedRunTimer.FormSec(PBTime);
            min.Text = SpeedRunTimer.FormMin(PBTime);
        }
    }
}
