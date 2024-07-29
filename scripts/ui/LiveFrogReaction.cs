public partial class LiveFrogReaction : Toggleable
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateReaction();
	}

	// Updates the visibility of the frog accordingly
	private void UpdateReaction()
	{
		if(ToggleReaction.HaveReaction)
			Open();
		else
			Close();
	}	

}
