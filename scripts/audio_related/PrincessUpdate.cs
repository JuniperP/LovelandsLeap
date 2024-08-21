using Godot;

public enum GameState : byte
{
	Percent25,
	Percent50,
	Percent75,
}

public partial class PrincessUpdate : AudioStreamPlayer
{
	// Getting how far the player is in the game
	[Export] public GameState PlayerReached;

	[ExportGroup("AudioToPlay")]
	[Export] public AudioStream StartEnd;
	[Export] public AudioStream Halfway;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Setting up to play the correct sound effect with -3db being default sound
		if (PlayerReached == GameState.Percent50)
		{
			Stream = Halfway;
			VolumeDb = -6;
		}
		else
		{
			Stream = StartEnd;
			if (PlayerReached == GameState.Percent25)
				VolumeDb = -9;
		}

	}

	// To be used connected to loading screen fading out to play the sfx
	public void PlayPrincessUpdate()
	{
		Play();
	}

}
