using Godot;

// Oscillates a light's offset from one point to another
public partial class LightOscillator : PointLight2D
{
	// Time to fade back and forth
	[Export] public double FadePeriod = 10d;
	[Export] public Vector2 StartPoint = new(-20, -20);
	[Export] public Vector2 EndPoint = new(20, 20);

	private double _currentTime = 0d;

	public override void _Process(double delta)
	{
		// Double the ratio so that half way is the end point
		float timeRatio = 2 * (float)(_currentTime / FadePeriod);

		// Interpolate and set new position
		Offset = StartPoint.Lerp(EndPoint, TrigSmooth01(timeRatio));

		_currentTime += delta;
	}

	// Returns a value between 0 and 1 on a sinusoidal curve with period 2
	private static float TrigSmooth01(float t)
	{
		return -0.5f * Mathf.Cos(Mathf.Pi * t) + 0.5f;
	}
}
