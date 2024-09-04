using Godot;

// Text that fades based on how close the target is
public partial class FadingText : Control
{
    [Export] Node2D target; // The node to calculate distance to
    [Export] float visibleRadius = 200; // The radius that text is visible within
    [Export] float invisibleRadius = 250; // The radius that text becomes invisible outside of

    public override void _Process(double delta)
    {
        float distance = Position.DistanceTo(target.Position);

        // TODO: Set modulation based on distance
    }
}
