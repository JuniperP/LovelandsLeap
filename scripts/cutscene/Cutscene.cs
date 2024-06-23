using Godot;
using System;

public partial class Cutscene : Node2D
{
	[Export] public bool Autoplay = true;
	[Export] public double NullWaitTime = 1d;
	[Export] public PackedScene NextScene;
	[Export] public PackedScene CancelScene;
	[Export] public Node[] Nodes;

	private ICutAnimatable[] _animNodes;
	private Timer _timer;
	private int _current;

	public override void _Ready()
	{
		_animNodes = new ICutAnimatable[Nodes.Length];
		for (int i = 0; i < Nodes.Length; i++)
		{
			try
			{
				if (Nodes[i] is not null)
					_animNodes[i] = (ICutAnimatable)Nodes[i];
			}
			catch (InvalidCastException e)
			{
				throw new NotSupportedException(
					"Provided 'Nodes' must implement ICutAnimatable", e
				);
			}
		}

		_timer = new Timer() { OneShot = true };
		_timer.Timeout += Step;
		AddChild(_timer);

		if (Autoplay)
			Run();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			PackedScene transition = CancelScene ?? NextScene;
			if (transition is not null)
				GetTree().ChangeSceneToPacked(transition);
        }
    }

	public void Run()
	{
		_current = 0;
		Step();
	}

	public void Step()
	{
		// If there are still animations left
		if (_current < _animNodes.Length)
		{
			// Get time till next animation
			double nextTrigger;
			if (_animNodes[_current] is null)
				nextTrigger = NullWaitTime;
			else
				nextTrigger = _animNodes[_current].TriggerAnimation();

			// Perform next animation in appropriate time
			_current++;
			if (nextTrigger > 0d)
				_timer.Start(nextTrigger);
			else
				Step();
		}
		else if (NextScene is not null)
			GetTree().ChangeSceneToPacked(NextScene);
	}
}
