using Godot;
using System;

public partial class EnvironmentalShader : ColorRect
{
	public static Color Tint
	{
		get { return (Color)_shaderMaterial.GetShaderParameter("tint"); }
		set { _shaderMaterial.SetShaderParameter("tint", value); }
	}

	public static Vector3 HSV
	{
		get
		{
			return new Vector3(
				(float)_shaderMaterial.GetShaderParameter("h"),
				(float)_shaderMaterial.GetShaderParameter("s"),
				(float)_shaderMaterial.GetShaderParameter("v")
			);
		}
		set
		{
			_shaderMaterial.SetShaderParameter("h", value.X);
			_shaderMaterial.SetShaderParameter("s", value.Y);
			_shaderMaterial.SetShaderParameter("v", value.Z);
		}
	}

	private static ShaderMaterial _shaderMaterial;

	public override void _Ready()
	{
		// Get the shader material as a static variable
		if (Material is ShaderMaterial material)
			_shaderMaterial = material;
		else
			throw new InvalidOperationException("The provided material was not a shader.");
	}
}
