using Godot;
using System;

public partial class Cutscene : Node2D
{
	[Export] public bool Autoplay = true;
	[Export] public double NullWaitTime = 1d;
	[Export] public PackedScene NextScene;
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
		// TODO: Use UI escape key to skip
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
			if (_animNodes[_current] is null)
				_timer.Start(NullWaitTime);
			else
			{
				double nextTrigger = _animNodes[_current].TriggerAnimation();
				_timer.Start(nextTrigger);
			}
			_current++;
		}
		else if (NextScene is not null)
			GetTree().ChangeSceneToPacked(NextScene);
	}
}
