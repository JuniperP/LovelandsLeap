using Godot;
using System;

public partial class SpeedRunTimer : Toggleable
{
	// Total time elapsed
	private static float _timeElapsed = 0;

	// To see if a run is currently happening 
	private static bool _currentlyRunning = false;

	// Creating a max value
	private static bool _belowMax = true;

	// Components of the clock
	private Label _minutes;
	private Label _seconds;
	private Label _centiseconds;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_minutes = GetNode<Label>("Minutes");
		_seconds = GetNode<Label>("Seconds");
		_centiseconds = GetNode<Label>("Centiseconds");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Changing visibility accordingly
		if (ToggleSpeedrun.HasTimer && !Visible)
			Open();

		if (!ToggleSpeedrun.HasTimer && Visible)
		{
			Close();
			_centiseconds.Text = "00";
			_seconds.Text = "00";
			_minutes.Text = "00";
		}

		// Accounting for a max amount
		if (_belowMax)
		{

			// Increasing the time
			if (_currentlyRunning)
				_timeElapsed += (float)delta;

			// Updating the clock
			if (Visible)
			{
				// Centiseconds excluding the "#."
				_centiseconds.Text = FormCentSec(_timeElapsed);

				// Seconds
				_seconds.Text = FormSec(_timeElapsed);

				// Minutes
				_minutes.Text = FormMin(_timeElapsed);
			}

			// Ensuring the max wasn't met
			if (_timeElapsed >= 6000)
				_belowMax = false;
		}

		else
		{
			_centiseconds.Text = "99";
			_seconds.Text = "59";
			_minutes.Text = "99";
		}

	}

	// Helper functions for making the time
	// Gets Centiseconds
	public static string FormCentSec(float _timeElapsed)
	{
		int centiSec = (int)(100 * MathF.Round(_timeElapsed % 1f, 2, MidpointRounding.ToZero));
		return centiSec.ToString("D2");
	}

	// Gets seconds
	public static string FormSec(float _timeElapsed)
	{
		return ((int)_timeElapsed % 60).ToString("D2");
	}

	// Gets minutes
	public static string FormMin(float _timeElapsed)
	{
		return ((int)(_timeElapsed / 60)).ToString("D2");
	}

	// Activating the speedrun methods
	// Starts a speedrun
	public static void StartSpeedrun()
	{
		if (ToggleSpeedrun.HasTimer)
			_currentlyRunning = true;
	}

	// Ends the current speedrun
	public static void FinishedRun()
	{
		_currentlyRunning = false;

		// Save record
		ToggleSpeedrun.NewTime(_timeElapsed);
	}

	// Resets the current speedrun
	public static void ResetRun()
	{
		_currentlyRunning = false;
		_timeElapsed = 0;
		_belowMax = true;
	}
}
