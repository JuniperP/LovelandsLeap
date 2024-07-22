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
			Open();

		if (!ToggleSpeedrun.HaveTimer && Visible)
		{
			Close();
			_milliseconds.Text = "00";
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
				// Milliseconds excluding the "#."
				string ourNum = $"{MathF.Round(_timeElapsed % 1f, 2)}";
				if (ourNum.Length >= 2)
					_milliseconds.Text = ourNum.Substring(ourNum.Length - 2);


				// Seconds
				_seconds.Text = FormatWithStart0($"{(int)(_timeElapsed % 60)}");

				// Minutes
				_minutes.Text = FormatWithStart0($"{(int)(_timeElapsed / 60)}");
			}

			// Ensuring the max wasn't met
			if (_timeElapsed >= 6000)
				_belowMax = false;
		}

		else
		{
			_milliseconds.Text = "99";
			_seconds.Text = "99";
			_minutes.Text = "99";
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
	public static void StartSpeedrun()
	{
		if (ToggleSpeedrun.HaveTimer)
			_currentlyRunning = true;
	}

	// Ends the current speedrun
	public static void FinishedRun()
	{
		_currentlyRunning = false;
		_belowMax = true;

		// Save record?
	}

	// Resets the current speedrun
	public static void ResetRun()
	{
		_currentlyRunning = false;
		_timeElapsed = 0;
		_belowMax = true;
	}
}
