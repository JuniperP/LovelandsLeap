using System;
using Godot;

public partial class CutscenePause : Timer, ICutsceneElement
{
    public override void _Ready()
    {
        OneShot = true;
    }

    public void Step(Action callback)
    {
        // Start the timer and perform callback upon timeout
        if (TimeLeft <= 0d)
        {
            Timeout += callback;
            Start();
        }
        // Stop the timer and perform callback
        else
        {
            Stop();
            callback();
        }
    }
}
