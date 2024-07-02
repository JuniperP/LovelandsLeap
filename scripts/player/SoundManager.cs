using Godot;
using System;
using System.Collections.Generic;


// Enums for all of the possible name
public enum SFX :  byte
{
	Walk,
	Jump,
	Land,
	TongueShoot,
	TongueHit,
	Dialogue,
	UIButton,
}


public partial class SoundManager : Node
{
	// Dictionary for comprehensive access to each sfx
	private static readonly Dictionary<SFX, AudioStreamPlayer> _sounds = new()
	{
		{SFX.Walk, _create_player(SFX.Walk)},
		{SFX.Jump,  _create_player(SFX.Jump)},
		{SFX.Land,  _create_player(SFX.Land)},
		{SFX.TongueShoot,  _create_player(SFX.TongueShoot)},
		{SFX.TongueHit, _create_player(SFX.TongueHit)},
		{SFX.Dialogue,  _create_player(SFX.Dialogue)},
		{SFX.UIButton,  _create_player(SFX.UIButton)},
	};

	// Method to create our audio sources for our dictionary
	private static AudioStreamPlayer _create_player(SFX sfx)
	{
		// Creates our player to return
		AudioStreamPlayer NewPlayer = new AudioStreamPlayer();

		// Ensures we are only working with sound effects
		NewPlayer.Bus = "Sound Effects";

		// Giant if statement mess cause I couldn't think of a better way to assign names and files
		if(sfx==SFX.Walk)
			NewPlayer.Name = "_temporary_sfx";
		else if(sfx==SFX.Jump)
			NewPlayer.Name = "jump_sfx";
		else if(sfx==SFX.Land)
			NewPlayer.Name = "_temporary_sfx";
		else if(sfx==SFX.TongueShoot)
			NewPlayer.Name = "tongue_shoot_sfx";
		else if(sfx==SFX.TongueHit)
			NewPlayer.Name = "tongue_hit_sfx";
		else if(sfx==SFX.Dialogue)
			NewPlayer.Name = "dialogue_sfx";
		else if(sfx==SFX.UIButton)
			NewPlayer.Name = "_temporary_sfx";
			
		
		// Assigning the file for each sound
		NewPlayer.Stream = (AudioStream) GD.Load($"res://audio/sfx/{NewPlayer.Name}.wav");

		// Gives back our new player
		return NewPlayer;
	}


	// Client method to easily play sounds from anywhere
	public static void PlaySound(SFX Sound, Node PlayOn)
	{
		// Adds the sound to the node tree if not already there
		if(PlayOn.FindChild(_sounds[Sound].Name) == null)
		{
			PlayOn.AddChild(_sounds[Sound]);
		}

		// Plays the sound effect
		_sounds[Sound].Play();
	}



	// Recursive post-order depth 1st search through children assigning sound effects to UI buttons
	private static void ApplyButtonSFX(Node node)
	{
		//Goes through each child
			//Calls this function again but with each child
			//Checks if child is a button
				//If is apply signal to do lambda that does: SoundManager.PlaySound(SoundManager.SFX.UIButton);

	}

	/*
		IDEA:
		Will be only called during Loadin, the single time scene that is only accessible when the game
		starts up. This will also be where other entering game features happen like loading settings data.
	*/

}
