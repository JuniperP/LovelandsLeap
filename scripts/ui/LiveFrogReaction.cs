public partial class LiveFrogReaction : Toggleable
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ToggleReaction.ChangeFrogFace = true;
	}

	/*
	I wish this could just be a signal connected via enabling editable children
	from button to this scene...
	BUT NOOOOO!
	Godot is being cringe!
	This is why we can't have nice things, Godot just has to make a HUGE clutter
	in the scene tree and give terrible error messages!
	Fine! Have it your way Godot >:( !
	*/
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (ToggleReaction.ChangeFrogFace)
		{
			// Updates the visibility of the frog accordingly
			if (ToggleReaction.HaveReaction)
				Open();
			else
				Close();

			ToggleReaction.ChangeFrogFace = false;
		}
	}

}
