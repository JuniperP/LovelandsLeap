using Godot;

/*
 Any UI node that should be toggled on and off can be a sub class of this. This allows
 for easy signal interactions as one can toggle the visibility by calls to _close()
 and open().

 Created before I know about canvas items Show() and Hide(), but since the ready function
 is helpful and and so many things are linked to Open() and Close() we're keeping it.
*/
public partial class Toggleable : Control
{
	// By default, sets visibility to closed
	public override void _Ready()
	{
		Close();
	}

	// Closes the node
	protected virtual void Close()
	{
		Visible = false;
	}

	// Open the node
	protected virtual void Open()
	{
		Visible = true;
	}
}
