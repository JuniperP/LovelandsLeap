public partial class LiveFrogReaction : Toggleable
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateReaction();
	}

	
	private void UpdateReaction()
	{
		// Updates the visibility of the frog accordingly
		if (ToggleReaction.HaveReaction)
			Open();
		else
			Close();
	}

}
