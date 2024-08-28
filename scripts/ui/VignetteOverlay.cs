using Godot;
using System;

public partial class VignetteOverlay : TextureRect
{
	// Time of the half cycle (start to end)
	[Export] public double FadePeriod = 10;
	[Export] public float StartStrength = 0.9f;
	[Export] public float EndStrength = 1.2f;

	private ShaderMaterial _shaderMaterial;
	private double _currentTime = 0d;

	public override void _Ready()
	{
		if (Material is ShaderMaterial material)
			_shaderMaterial = material;
		else
			throw new InvalidOperationException("The provided material was not a shader.");
	}

	public override void _Process(double delta)
	{
		// Double the ratio so that half way is the end strength
		float timeRatio = 2 * (float)(_currentTime / FadePeriod);

		// Interpolate and set new strength
		float newStrength = TrigSmooth(timeRatio, StartStrength, EndStrength);
		_shaderMaterial.SetShaderParameter("vignette_strength", newStrength);

		_currentTime += delta;
	}

	private static float TrigSmooth(float t, float a, float b)
	{
		return a + (b - a) * (-0.5f * Mathf.Cos(Mathf.Pi * t) + 0.5f);
	}
}
