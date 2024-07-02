using Godot;
using System;
using System.Collections.Generic;


// Enums for all of the possible name
public enum SFX : byte
{
	Walk,
	Jump,
	Land,
	TongueShoot,
	TongueHit,
	Croak,
	DialogueMC,
	DialogueFrog,
	DialogueWitch,
	DialoguePrincess,
	UIButton,
}


public partial class SoundManager : Node
{
	// Dictionary for comprehensive access to each sfx
	private static readonly Dictionary<SFX, AudioStreamPlayer> _sounds = new()
	{
		{SFX.Walk, _create_audio_player(SFX.Walk)},
		{SFX.Jump,  _create_audio_player(SFX.Jump)},
		{SFX.Land,  _create_audio_player(SFX.Land)},
		{SFX.Croak,  _create_audio_player(SFX.Croak)},
		{SFX.TongueShoot,  _create_audio_player(SFX.TongueShoot)},
		{SFX.TongueHit, _create_audio_player(SFX.TongueHit)},
		{SFX.DialogueMC,  _create_audio_player(SFX.DialogueMC)},
		{SFX.DialogueFrog,  _create_audio_player(SFX.DialogueFrog)},
		{SFX.DialogueWitch,  _create_audio_player(SFX.DialogueWitch)},
		{SFX.DialoguePrincess,  _create_audio_player(SFX.DialoguePrincess)},
		{SFX.UIButton,  _create_audio_player(SFX.UIButton)},
	};

	// Method to create our audio sources for our dictionary
	private static AudioStreamPlayer _create_audio_player(SFX sfx)
	{
		// Creates our player to return
		AudioStreamPlayer NewPlayer = new AudioStreamPlayer();

		// Ensures we are only working with sound effects
		NewPlayer.Bus = "Sound Effects";

		// Giant if statement mess cause I couldn't think of a better way to assign names and files
		if (sfx == SFX.Walk)
			NewPlayer.Name = "temporary_sfx";
		else if (sfx == SFX.Jump)
			NewPlayer.Name = "jump_sfx";
		else if (sfx == SFX.Land)
			NewPlayer.Name = "temporary_sfx";
		else if (sfx == SFX.TongueShoot)
			NewPlayer.Name = "tongue_shoot_sfx";
		else if (sfx == SFX.TongueHit)
			NewPlayer.Name = "tongue_hit_sfx";
		else if (sfx == SFX.Croak)
			NewPlayer.Name = "temporary_sfx";
		else if (sfx == SFX.DialogueMC)
			NewPlayer.Name = "dialogue_sfx";
		else if (sfx == SFX.DialogueFrog)
			NewPlayer.Name = "dialogue_sfx";
		else if (sfx == SFX.DialogueWitch)
			NewPlayer.Name = "dialogue_sfx";
		else if (sfx == SFX.DialoguePrincess)
			NewPlayer.Name = "dialogue_sfx";
		else if (sfx == SFX.UIButton)
			NewPlayer.Name = "ui_buttons_sfx";


		// Assigning the file for each sound
		NewPlayer.Stream = (AudioStream)GD.Load($"res://audio/sfx/{NewPlayer.Name}.wav");

		// Gives back our new player
		return NewPlayer;
	}


	// Client method to easily play sounds from anywhere
	public static void PlaySound(SFX Sound, Node PlayOn)
	{
		// Adds the sound to the node tree if not already there
		if (PlayOn.FindChild(_sounds[Sound].Name, false, false) == null)
		{
			PlayOn.AddChild(_sounds[Sound]);
		}

		// Plays the sound effect
		_sounds[Sound].Play();


	}

	// Recursive post-order depth 1st search through children assigning sound effects to UI buttons
	public static void ApplyButtonSFX(Node CurrentNode, bool first)
	{
		// Ensuring all children have access to the sound to play
		if(first)
		{
			CurrentNode.AddChild(_sounds[SFX.UIButton]);
			first = false;
		}

		// Getting all of the children
		Godot.Collections.Array<Node> Children = CurrentNode.GetChildren();

		for (int i = 0; i < Children.Count; i++)
		{
			
			// Recursively going down as far as possible into the tree
			ApplyButtonSFX(Children[i], first);

			// If a button is found it is set up to make a sound on click
			if (Children[i].GetClass() == "Button")
				((Button)CurrentNode.GetChildren()[i]).Pressed += () => _sounds[SFX.UIButton].Play();

		}
		
	}

	/*
		IDEA:
		Will be only called during Loadin, the single time scene that is only accessible when the game
		starts up. This will also be where other entering game features happen like loading settings data.
	*/

}
