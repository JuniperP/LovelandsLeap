using Godot;
using Godot.Collections;
using System;

public enum MusicID
{
	Cutscene,
	Platforming,
	MainMenu,
	Abomination,
}

public partial class GlobalMusicPlayer : AudioStreamPlayer
{
	private readonly Dictionary<MusicID, string> _audioPaths = new()
	{
		{MusicID.Cutscene, "loveland_cutscene_music"},
		{MusicID.Platforming, "loveland_platforming_music"},
		{MusicID.MainMenu, "loveland_menu_music"},
		{MusicID.Abomination, "abomination"},
	};

	// Create a pseudo-singleton
	private static GlobalMusicPlayer _instance;
	public GlobalMusicPlayer()
	{
		_instance = this;
	}

	// Forms the path name for some audio file 
	private static string FormPath(string unique)
	{
		return $"res://audio/music/{unique}.wav";
	}

	// TODO: Do a valid node check before accessing instance instead of catching an error
	// Call the method on the autoloaded instance
	public static void PlayMusic(MusicID id)
	{
		try
		{
			_instance.InternalPlayMusic(id);
		}
		catch (NullReferenceException e)
		{
			throw new InvalidOperationException(
				"There is no GlobalMusicPlayer object. Is the script autoloaded?", e
			);
		}
	}

	private void InternalPlayMusic(MusicID id)
	{
		AudioStream newStream = GD.Load<AudioStream>(FormPath(_audioPaths[id]));

		// If new stream is different, play the new stream
		if (Stream != newStream)
		{
			Stream = newStream;
			Play();
		}
	}

	// Call the method on the autoloaded instance
	public static void StopMusic()
	{
		try
		{
			_instance.Stop();
			_instance.Stream = null;
		}
		catch (NullReferenceException e)
		{
			throw new InvalidOperationException(
				"There is no GlobalMusicPlayer object. Is the script autoloaded?", e
			);
		}
	}

	// Call the method on the autoloaded instance
	public static void PauseMusic()
	{
		try
		{
			_instance.StreamPaused = true;
		}
		catch (NullReferenceException e)
		{
			throw new InvalidOperationException(
				"There is no GlobalMusicPlayer object. Is the script autoloaded?", e
			);
		}
	}

	// Call the method on the autoloaded instance
	public static void UnpauseMusic()
	{
		try
		{
			_instance.StreamPaused = false;
		}
		catch (NullReferenceException e)
		{
			throw new InvalidOperationException(
				"There is no GlobalMusicPlayer object. Is the script autoloaded?", e
			);
		}
	}
}
