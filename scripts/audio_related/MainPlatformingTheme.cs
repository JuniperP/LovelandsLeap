using Godot;

public partial class MainPlatformingTheme : AudioStreamPlayer2D
{
	// Easy calls to stop and play the music
	public void Start()
	{
		Play();
	}

	public void End()
	{
		Stop();
	}


}
