using Godot;
using System;
/*
 Any UI node that should be toggled on and off can be a sub class of this. This allows
 for easy signal interactions as one can toggle the visibility by calls to _close()
 and open().
*/
public abstract partial class Toggleable : Control
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
