using Godot;


public partial class InGameBrightnessLabel : Label
{
	// Other nodes in brightness sequence
	[Export] public HSlider Slider;
	[Export] public Label Percentage;

	// The environment we are adjusting
	private Environment environment;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		environment = GetTree().Root.GetNode<WorldEnvironment>("GlobalScreenEffects").Environment;
	}

	// Updating the current info
	private void Update()
	{
		// Sets sliders current value to match the world values brightness
		if (Visible)
			UpdateBrightness(environment.AdjustmentBrightness);

	}

	private void UpdateBrightness(float newVal)
	{
		environment.AdjustmentBrightness = newVal;

		Percentage.Text = $"{Mathf.Round(newVal / 2 * 100)}%";
	}

}
