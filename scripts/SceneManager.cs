using Godot;
using System.Collections.Generic;

// Enums for all of the possible scenes transitioned to
public enum ToScene : byte
{
	MainMenu,
	Credits,
	IntroCutscene,
	PlayTestLevel,

}
public partial class SceneManager : Node
{
	/* 
		Dictionary of strings for paths of scenes that will be changed to.
		This gives us only a single place by which we must update the paths names.

		Note: Not currently using exports of packed scenes as they chain to load each other,
		thus causing an infinite loop when if one scene tries going to another. (also starts
		corrupting scenes!)
	*/
	private static readonly Dictionary<ToScene, string> _scenes = new()
	{
		{ToScene.MainMenu, "res://scenes/ui/main_menu.tscn"},
		{ToScene.Credits,  "res://scenes/ui/credits.tscn"},
		{ToScene.IntroCutscene,  "res://scenes/cutscenes/intro.tscn"},
		{ToScene.PlayTestLevel,  "res://scenes/levels/play_test.tscn"},
	};

	// Allows client to easily get paths to any scene
	public static string GetPath(ToScene scene) 
	{
		return _scenes[scene];
	}
}
