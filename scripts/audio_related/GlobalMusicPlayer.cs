using Godot;
using Godot.Collections;
using System;

public enum MusicID
{
	Cutscene,
	MainMenu,
	Abomination,
	Tutorial,
	Sunset,
	Forest,
	DeepWoods,
	TheOldestTree,
}

public partial class GlobalMusicPlayer : AudioStreamPlayer
{
	private readonly Dictionary<MusicID, string> _audioPaths = new()
	{
		{MusicID.Cutscene, "cutscene_ambience"},
		{MusicID.MainMenu, "menu_music"},
		{MusicID.Abomination, "abomination"},
		{MusicID.Tutorial, "tutorial_music"},
		{MusicID.Sunset, "sunset_music"},
		{MusicID.Forest, "forest_music"},
		{MusicID.DeepWoods, "deep_woods_music"},
		{MusicID.TheOldestTree, "the_oldest_tree_music"},
	};

	// Create a singleton
	private static GlobalMusicPlayer _instance;
	public GlobalMusicPlayer()
	{
		// Raise an error if there is already an instance, otherwise set this as the instance
		if (_instance.IsValid())
			throw new InvalidOperationException(
				"There is already a GlobalMusicPlayer. There cannot be multiple instances."
			);
		else
			_instance = this;
	}

	// Forms the path name for some audio file 
	private static string FormPath(string unique)
	{
		return $"res://audio/music/{unique}.wav";
	}

	public static void PlayMusic(MusicID id)
	{
		CheckInstance();
		_instance.InternalPlayMusic(id);
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

	public static void StopMusic()
	{
		CheckInstance();
		_instance.Stop();
		_instance.Stream = null;
	}

	public static void PauseMusic()
	{
		CheckInstance();
		_instance.StreamPaused = true;
	}

	public static void UnpauseMusic()
	{
		CheckInstance();
		_instance.StreamPaused = false;
	}

	private static void CheckInstance()
	{
		// Check for singleton instance
		if (!_instance.IsValid())
			throw new InvalidOperationException(
				"There is no GlobalMusicPlayer object. Is the script autoloaded?"
			);
	}
}
