using Godot;
using System;

public partial class Cutscene : Node2D
{
	[Export] public bool Autoplay = true;
	[Export] public Node[] Nodes;
	[Export] public PackedScene NextScene;

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
			double nextTrigger = _animNodes[_current].TriggerAnimation();
			_timer.Start(nextTrigger);
			_current++;
		}
	}
}
