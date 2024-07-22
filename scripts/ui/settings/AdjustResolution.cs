using Godot;
using System;


public partial class AdjustResolution : OptionButton
{
	// The users selected Resolution option
	public static int userID = -1;



	// Done at start of game and when display changes
	public void PopulateOptions()
	{
		// Destroy current resolution options
		Clear();

		// Populate drop down with new resolutions
		// AddItem("test", 0);

		// Select default option
		Select(0);
	}


	// Signaled when new drop down option is selected
	public static void NewResolutionSet(int id)
	{
		userID = id;

		// AdjustScreenResolution(get(userID));

	}

	// Helper func to set resolution
	public static void AdjustScreenResolution(/*Resolution res*/)
	{
		// New resolution = res
	}

}

