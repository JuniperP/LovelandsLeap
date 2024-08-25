using Godot;
using System.Collections.Generic;

// Enums for all of the possible scenes transitioned to
public enum ToScene : int
{
	MainMenu = -1,
	Credits = -2,
	IntroCutscene = -3,
	Monitor = -4,
	BlackBackDrop = -5,
	SplashScreen = -6,
	PlaceHolder = -7,
	PlayTestLevel = 0,
	Tutorial = 1,
	Level1 = 2,
	Level2 = 3,
	Level3 = 4,
	Level4 = 5,

}
public partial class SceneManager : Node
{
	// Where we will be changing scenes to
	private static ToScene _goTo = ToScene.PlaceHolder;

	/* 
		Dictionary of strings for paths of scenes that will be changed to.
		This gives us only a single place by which we must update the paths names.

		Note: Not currently using exports of packed scenes as they chain to load each other,
		thus causing an infinite loop when if one scene tries going to another. (Also starts
		corrupting scenes!)
	*/
	public static readonly Dictionary<ToScene, string> _scenes = new()
	{
		{ToScene.MainMenu, "res://scenes/ui/main_menu.tscn"},
		{ToScene.Credits,  "res://scenes/credits.tscn"},
		{ToScene.IntroCutscene,  "res://scenes/cutscenes/intro.tscn"},
		{ToScene.BlackBackDrop,  "res://scenes/almost_black_backdrop.tscn"},
		{ToScene.SplashScreen,  "res://scenes/load_game_in.tscn"},
		{ToScene.Monitor,  "res://scenes/ui/settings/monitor_stand_in.tscn"},
		{ToScene.PlayTestLevel,  "res://scenes/levels/play_test.tscn"},
		{ToScene.Tutorial,  "res://scenes/levels/tutorial.tscn"},
		{ToScene.Level1,  "res://scenes/levels/level_1.tscn"},
		{ToScene.Level2,  "res://scenes/levels/temp_level.tscn"},
		{ToScene.Level3,  "res://scenes/levels/level_3.tscn"},
		{ToScene.Level4,  "res://scenes/levels/level_4.tscn"},
	};


	// Allows the client to set where to go to next
	public static void SetNextGoTo(ToScene scene)
	{
		_goTo = scene;
	}

	// Sends user to new scene
	public static void GoToSetScene(Node useNode)
	{

		// If asked, the scene switches
		if (_goTo != ToScene.PlaceHolder)
		{
			// Loading in a backdrop to hide the debug boxes
			PackedScene loadingScene = (PackedScene) ResourceLoader.Load(_scenes[ToScene.BlackBackDrop]);
			Node loadingScreen = loadingScene.Instantiate();
			useNode.GetTree().Root.CallDeferred("add_child", loadingScreen);

			// Switching scenes
			useNode.GetTree().ChangeSceneToFile(_scenes[_goTo]);

			// Getting rid of the hiding box
			loadingScreen.QueueFree();
		}
	}

	// Niche case getters
	public static string GetPath(ToScene scene)
	{
		return _scenes[scene];
	}

	public static ToScene GetNextGoTo()
	{
		return _goTo;
	}
}
