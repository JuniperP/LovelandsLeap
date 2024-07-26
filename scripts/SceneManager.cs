using Godot;
using System.Collections.Generic;

// Enums for all of the possible scenes transitioned to
public enum ToScene : int
{
	MainMenu = 1,
	Credits = 2,
	IntroCutscene = 3,
	Monitor = 4,
	PlayTestLevel = 5 ,
	Level1 = 6,

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

	// Where we will be changing scenes to
	private static string _goTo = "";

	private static readonly Dictionary<ToScene, string> _scenes = new()
	{
		{ToScene.MainMenu, "res://scenes/ui/main_menu.tscn"},
		{ToScene.Credits,  "res://scenes/credits.tscn"},
		{ToScene.IntroCutscene,  "res://scenes/cutscenes/intro.tscn"},
		{ToScene.Monitor,  "res://scenes/ui/settings/monitor_stand_in.tscn"},
		{ToScene.PlayTestLevel,  "res://scenes/levels/play_test.tscn"},
		{ToScene.Level1,  "res://scenes/levels/level_1.tscn"},
	};

	// Allows the client to set where to go to next
	public static void SetNextGoTo(ToScene scene)
	{
		_goTo = _scenes[scene];
	}

	// Sends user to new scene
	public static void GoToSetScene(Node useNode)
	{
		// If asked, the scene switches
		if (!_goTo.Equals(""))
		{
			useNode.GetTree().ChangeSceneToFile(_goTo);
			_goTo = "";
		}
	}

	// Getter for niche cases
	public static string GetPath(ToScene scene)
	{
		return _scenes[scene];
	}
}
