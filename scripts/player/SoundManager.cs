using Godot;
using System.Collections.Generic;

public enum SFX : byte
{
	Walk,
	Jump,
	Land,
	TongueShoot,
	TongueHit,
	Croak,
	Dialogue,
}

public class SoundManager
{
	private readonly Dictionary<SFX, string> _soundNames = new()
	{
		{SFX.Walk, "TODO"},
		// TODO: Write in sfx strings
	};
}
