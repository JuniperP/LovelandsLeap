using Godot;
using System;

public partial class SpeedRunTimer : Toggleable
{
	// Total time elapsed
	private static float _timeElapsed = 0;

	// To see if a run is currently happening 
	private static bool _currentlyRunning = false;

	// Components of the clock
	private Label _minutes;
	private Label _seconds;
	private Label _milliseconds;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_minutes = GetNode<Label>("Minutes");
		_seconds = GetNode<Label>("Seconds");
		_milliseconds = GetNode<Label>("Milliseconds");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Changing visibility accordingly
		if (ToggleSpeedrun.HaveTimer && !Visible)
		{
			Open();
			_currentlyRunning = true;
		}

		if (!ToggleSpeedrun.HaveTimer && Visible)
			Close();

		// Increasing the time
		if (_currentlyRunning)
			_timeElapsed += (float)delta;

		// Updating the clock
		if (Visible)
		{
			// Milliseconds excluding the "#."
			string ourNum = $"{MathF.Round(_timeElapsed % 1f, 2)}";
			if (ourNum.Length >= 2)
				_milliseconds.Text = ourNum.Substring(ourNum.Length - 2);


			// Seconds
			_seconds.Text = FormatWithStart0($"{(int)(_timeElapsed % 60)}");

			// Minutes
			_minutes.Text = FormatWithStart0($"{(int)(_timeElapsed / 60)}");

		}

	}

	// Helps to make stylistic format adjustments to our labels to display
	private string FormatWithStart0(string ourNum)
	{
		// Adding an extra 0.
		if (ourNum.Length < 2)
			ourNum = $"0{ourNum}";

		return ourNum;
	}


	// Starts a speedrun
	private void StartSpeedrun()
	{
		if (ToggleSpeedrun.HaveTimer)
			_currentlyRunning = true;
	}

	// Ends the current speedrun
	private void FinishedRun()
	{
		_currentlyRunning = false;
	}

	// Resets the current speedrun
	private void ResetRun()
	{
		_timeElapsed = 0;
		_currentlyRunning = false;
	}
}
