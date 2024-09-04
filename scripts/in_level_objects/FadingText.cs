using Godot;

// Text that fades based on how close the target is
public partial class FadingText : Label
{
    [Export] public Node2D Target; // The node to calculate distance to
    [Export] public float VisibleRadius = 750f; // The radius that text is visible within
    [Export] public float InvisibleRadius = 1000f; // The radius that text becomes invisible outside of

    private Vector2 _centerPos;

    public void CalculateCenter()
    {
        _centerPos = GlobalPosition + Size / 2;

        // CollisionShape2D shape = new()
        // {
        //     Shape = new CircleShape2D(),
        // };
        // AddChild(shape);
        // shape.GlobalPosition = _centerPos;
    }

    public override void _Process(double delta)
    {
        float distance = _centerPos.DistanceTo(Target.GlobalPosition);

        // Set modulation based on distance
        float farnessRatio = (distance - VisibleRadius) / (InvisibleRadius - VisibleRadius);
        Modulate = new Color(1f, 1f, 1f, 1f - TrigSmooth01(farnessRatio));
    }

    // Returns a value between 0 and 1 on a sinusoidal curve with period 2
    private static float TrigSmooth01(float t)
    {
        t = Mathf.Clamp(t, 0f, 1f);
        return -0.5f * Mathf.Cos(Mathf.Pi * t) + 0.5f;
    }
}
