using Godot;
using System.Collections.Generic;

/* 
Recognize:
In the following sfx, no dialogue sfxs are included! Instead, all
dialogue sfx are imported directly within their respective cutscenes.
*/

// Enums for all of the possible names
public enum SFX : int
{
	Jump = 1,
	Land,
	TongueShoot,
	TongueHit,
	Croak,
	LongCroak,
	IntoFrog,
	UIButton,
	FlyCatch,
	WallBump,
	HouseDoor,
	Magic,
	Walk1,
	Walk2,
	Walk3,
	Walk4,
}

public partial class SoundManager : Node
{
	// Dictionary to easily get the prefix for a each sfx file name 
	private static readonly Dictionary<SFX, string> _sound_paths = new()
	{
		{SFX.Jump, "jump_sfx"},
		{SFX.Land, "land_sfx"},
		{SFX.Croak, "croak_sfx"},
		{SFX.LongCroak, "long_croak_sfx"},
		{SFX.IntoFrog, "into_frog_sfx"},
		{SFX.TongueShoot, "tongue_shoot_sfx"},
		{SFX.TongueHit, "tongue_hit_sfx"},
		{SFX.UIButton, "ui_button_sfx"},
		{SFX.FlyCatch, "fly_catch_sfx"},
		{SFX.WallBump, "wall_bump_sfx"},
		{SFX.Walk1, "walk_1_sfx"},
		{SFX.Walk2, "walk_2_sfx"},
		{SFX.Walk3, "walk_3_sfx"},
		{SFX.Walk4, "walk_4_sfx"},
	};

	// Dictionary for comprehensive access to each sfx
	private static readonly Dictionary<SFX, AudioStreamPlayer> _sounds = new()
	{
		{SFX.Jump,  CreateAudioPlayer(SFX.Jump)},
		{SFX.Land,  CreateAudioPlayer(SFX.Land)},
		{SFX.Croak,  CreateAudioPlayer(SFX.Croak)},
		{SFX.LongCroak,  CreateAudioPlayer(SFX.LongCroak)},
		{SFX.IntoFrog,  CreateAudioPlayer(SFX.IntoFrog)},
		{SFX.TongueShoot,  CreateAudioPlayer(SFX.TongueShoot)},
		{SFX.TongueHit, CreateAudioPlayer(SFX.TongueHit)},
		{SFX.UIButton,  CreateAudioPlayer(SFX.UIButton)},
		{SFX.FlyCatch, CreateAudioPlayer(SFX.FlyCatch)},
		{SFX.WallBump, CreateAudioPlayer(SFX.WallBump)},
		{SFX.Walk1, CreateAudioPlayer(SFX.Walk1)},
		{SFX.Walk2, CreateAudioPlayer(SFX.Walk2)},
		{SFX.Walk3, CreateAudioPlayer(SFX.Walk3)},
		{SFX.Walk4, CreateAudioPlayer(SFX.Walk4)},
	};

	// Method to create our audio sources for our dictionary
	private static AudioStreamPlayer CreateAudioPlayer(SFX sfx)
	{
		// Creates our player to return
		AudioStreamPlayer newPlayer = new AudioStreamPlayer();

		// Ensures we are only working with sound effects
		newPlayer.Bus = "Sound Effects";

		// Assigning the file for each sound
		newPlayer.Stream = GD.Load<AudioStream>(FormPath(_sound_paths[sfx]));

		// Gives back our new player
		return newPlayer;
	}

	// Client method to easily play sounds from anywhere
	public static void PlaySound(SFX sound, Node playOn, float volumeDB = 0f, float pitchScale = 1f)
	{
		// Makes new case of sfx
		AudioStreamPlayer toPlay = (AudioStreamPlayer)_sounds[sound].Duplicate();
		toPlay.VolumeDb = volumeDB;
		toPlay.PitchScale = pitchScale;

		// Set up sfx to explode ensuring no dispose issues, many AudioStreamPlayers, etc.
		toPlay.Finished += () => toPlay.QueueFree();

		// Adds the sfx
		playOn.AddChild(toPlay);

		// Plays the sfx
		toPlay.Play();
	}

	// Recursive post-order depth 1st search through children assigning sound effects to UI buttons
	public static void ApplyButtonSFX(Node currentNode)
	{
		// Getting all of the children
		Godot.Collections.Array<Node> children = currentNode.GetChildren();

		for (int i = 0; i < children.Count; i++)
		{
			// Recursively going down as far as possible into the tree
			ApplyButtonSFX(children[i]);

			// If a button or TabContainer is found it's set up to play sound on click (IDK WHY THESE USE i-1!!!!)
			if (children[i].GetClass() == "Button")
				((Button)children[i]).Pressed += () => PlaySound(SFX.UIButton, children[i - 1]);

			if (children[i].GetClass() == "TabContainer")
				((TabContainer)children[i]).TabClicked += (long NotUsed) => PlaySound(SFX.UIButton, children[i - 1]);
		}
	}

	// Forms the path name for some audio file 
	private static string FormPath(string unique)
	{
		return $"res://audio/sfx/{unique}.wav";
	}
}
