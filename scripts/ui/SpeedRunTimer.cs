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
		// Increasing the time
		if (_currentlyRunning)
			_timeElapsed += (float) delta;

		// Updating the clock
		if (Visible)
		{
			_minutes.Text = $"{MathF.Round(_timeElapsed / 60)}";
			_seconds.Text = $"{MathF.Round(_timeElapsed % 60f)}";
			_milliseconds.Text = $"{MathF.Round(_timeElapsed % 1f, 2)}";
		
		}

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
