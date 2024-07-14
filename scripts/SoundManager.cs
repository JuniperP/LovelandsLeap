using Godot;
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
	IntoFrog,
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
		{SFX.IntoFrog,  _create_audio_player(SFX.IntoFrog)},
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

		// Assigning the name
		NewPlayer.Name = $"{(sfx)}_sfx";

		// Assigning the file for each sound
		NewPlayer.Stream = (AudioStream)GD.Load($"res://audio/sfx/{NewPlayer.Name}.wav");

		// Gives back our new player
		return NewPlayer;
	}


	// Client method to easily play sounds from anywhere
	public static void PlaySound(SFX Sound, Node PlayOn)
	{

		// Makes new case of sound effect
		AudioStreamPlayer ToPlay = (AudioStreamPlayer)_sounds[Sound].Duplicate();

		// Set up sound effect to explode ensuring no dispose, many AudioStreamPlayer, etc issues
		ToPlay.Finished += () => ToPlay.QueueFree();

		// Adds the sound effect
		PlayOn.AddChild(ToPlay);

		// Plays the sound effect
		ToPlay.Play();

	}

	// Recursive post-order depth 1st search through children assigning sound effects to UI buttons
	public static void ApplyButtonSFX(Node CurrentNode)
	{

		// Getting all of the children
		Godot.Collections.Array<Node> Children = CurrentNode.GetChildren();

		for (int i = 0; i < Children.Count; i++)
		{
			// Recursively going down as far as possible into the tree
			ApplyButtonSFX(Children[i]);

			// If a button or TabContainer is found it's set up to play sound on click (IDK WHY THESE USE i-1!!!!)
			if (Children[i].GetClass() == "Button")
				((Button)Children[i]).Pressed += () => PlaySound(SFX.UIButton, Children[i - 1]);

			if (Children[i].GetClass() == "TabContainer")
				((TabContainer)Children[i]).TabClicked += (long NotUsed) => PlaySound(SFX.UIButton, Children[i - 1]);
		}

	}

}
