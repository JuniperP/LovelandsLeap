using Godot;
using System;
/*
 If I had time: this system would work via the decorator pattern, because
 using inheritance feels very limiting just to declare an action many nodes may use...
 HOWEVER, I am running on very little time (in different country) so... sorry :3!

 Idea = Any UI node that can be toggled on and off can be a sub class of this for easy
 signal interactions
*/
public partial class Toggleable : Control
{

	// Closes the node
	protected virtual void _close()
	{
		Visible = false;
	}

	// Open the node
	protected virtual void _open()
	{
		Visible = true;
	}

}
