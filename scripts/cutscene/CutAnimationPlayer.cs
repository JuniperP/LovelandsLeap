using System;
using Godot;
using Godot.Collections;

public partial class CutAnimationPlayer : AnimationPlayer, ICutsceneElement
{
    [Export] public string mainAnimation = "main";
    private Action _callback;

    public void Step(Action callback)
    {
        _callback = callback;

        // If currently playing, end animation and perform callback
        if (IsPlaying())
        {
            Seek(CurrentAnimationLength);
            Stop(true);
            callback();
        }
        // Otherwise, start the animation
        else
        {
            // Get main track and add method track for this node
            Animation main = GetAnimationLibrary("").GetAnimation(mainAnimation);
            int trackID = main.AddTrack(Animation.TrackType.Method);
            main.TrackSetPath(trackID, Name.ToString());

            // Insert callback method at the end of the animation
            main.TrackInsertKey(trackID, main.Length, new Dictionary()
            {
                {"method", nameof(PerformCallback)},
                {"args", new Godot.Collections.Array()}
            });

            Play(mainAnimation);
        }
    }

    public void PerformCallback()
    {
        _callback();
    }
}
