using Godot;

public partial class ToggleSpeedrun : ToggleButton
{
    // Whether or not the timer should be displayed
    public static bool haveTimer = false;

    // The players best speed run time (base = just above max)
    public static float pbTime = 6001;

    // Adjusting and resetting speedruns
    public override void Toggle()
    {
        if (!GetState())
            SpeedRunTimer.ResetRun();
    }

    protected override bool GetState()
    {
        return haveTimer;
    }

    protected override void SetState(bool state)
    {
        haveTimer = state;
    }

    // Allowing change to the pb time
    public static void NewTime(float time)
    {
        if (time < pbTime)
            pbTime = time;
    }

    // Updating users presented pb
    private void UpdateTime()
    {
        // Getting the parent of the children we need
        Label current = GetNode<Label>("CurrentTimeLabel");

        // Each time to change
        Label milSec = current.GetNode<Label>("Milliseconds");
        Label sec = current.GetNode<Label>("Seconds");
        Label min = current.GetNode<Label>("Minutes");

        // Accounting for no record
        if(pbTime == 6001)
        {
            milSec.Text = "na";
            sec.Text = "na";
            min.Text = "na";
        }
        else
        {
            // Displaying the users pb
            milSec.Text = SpeedRunTimer.FormMilSec(pbTime);
            sec.Text = SpeedRunTimer.FormSec(pbTime);
            min.Text = SpeedRunTimer.FormMin(pbTime);
        }
    }
}
