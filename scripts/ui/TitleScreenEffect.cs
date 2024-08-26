using Godot;
using System;

public partial class TitleScreenEffect : ParallaxBackground
{
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ScrollOffset += new Vector2((float)delta * -100, 0);
	}
}
