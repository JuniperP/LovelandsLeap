using Godot;
using Godot.Collections;
using System;

public enum MusicID
{
	CutsceneMusic
}

public partial class GlobalMusicPlayer : AudioStreamPlayer
{
	private readonly Dictionary<MusicID, string> _audioPaths = new()
	{
		{MusicID.CutsceneMusic, "loveland_cutscene_music"}
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
}
