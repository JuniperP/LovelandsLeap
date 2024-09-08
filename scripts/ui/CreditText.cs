using Godot;

public partial class CreditText : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		float time = SpeedRunTimer.TimeElapsed;
		int sceneCameFrom = (int)SceneManager.GetNextGoTo();

		// Special extra text if you just finished a speedrun
		if (time > 0 && sceneCameFrom >= 11)
		{
			string pbMessage = "";
			if(time == ToggleSpeedrun.PBTime)
				pbMessage = "\n\nA new personal best!";
			
			Text = $"\n\n\n\n\n\n\n\n\n\n\nEnd of run time:   {SpeedRunTimer.FormMin(time)} : {SpeedRunTimer.FormSec(time)} : {SpeedRunTimer.FormCentSec(time)}{pbMessage}{Text}";
			
			SpeedRunTimer.TimeElapsed = 0;
		}

	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Checks if at point in credits to cut the scrolling
		if (Position.Y > -(Size.Y - 1000))
		{
			// Scrolls and updates
			Position = new Vector2(Position.X, Position.Y - (120 * (float)delta));
		}
	}
}
