public partial class LiveFrogReaction : Toggleable
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ToggleReaction.ChangeFrogFace = true;
	}

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
