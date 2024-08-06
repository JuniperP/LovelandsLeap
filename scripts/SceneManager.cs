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
	LoadingScreen = -6,
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

	// Preloading screens for loading
	private static ResourcePreloader _preloader = new ResourcePreloader();

	/* 
		Dictionary of strings for paths of scenes that will be changed to.
		This gives us only a single place by which we must update the paths names.

		Note: Not currently using exports of packed scenes as they chain to load each other,
		thus causing an infinite loop when if one scene tries going to another. (Also starts
		corrupting scenes!)
	*/
	private static readonly Dictionary<ToScene, string> _scenes = new()
	{
		{ToScene.MainMenu, "res://scenes/ui/main_menu.tscn"},
		{ToScene.Credits,  "res://scenes/credits.tscn"},
		{ToScene.IntroCutscene,  "res://scenes/cutscenes/intro.tscn"},
		{ToScene.BlackBackDrop,  "res://scenes/almost_black_backdrop.tscn"},
		{ToScene.LoadingScreen,  "res://scenes/ui/loading_screen.tscn"},
		{ToScene.Monitor,  "res://scenes/ui/settings/monitor_stand_in.tscn"},
		{ToScene.PlayTestLevel,  "res://scenes/levels/play_test.tscn"},
		{ToScene.Tutorial,  "res://scenes/levels/tutorial.tscn"},
		{ToScene.Level1,  "res://scenes/levels/level_1.tscn"},
		{ToScene.Level2,  "res://scenes/levels/level_2.tscn"},
		{ToScene.Level3,  "res://scenes/levels/level_3.tscn"},
		{ToScene.Level4,  "res://scenes/levels/level_4.tscn"},
	};


	// Preloading the scenes we will have to use
	public static void PreloadForLoading()
	{
		_preloader.AddResource("BackDrop", ResourceLoader.Load(_scenes[ToScene.BlackBackDrop]));
	}


	// Allows the client to set where to go to next
	public static void SetNextGoTo(ToScene scene)
	{
		_goTo = scene;
	}

	// Sends user to new scene
	public static void GoToSetScene(Node useNode, bool useAsciiFrog)
	{

		// If asked, the scene switches
		if (_goTo != ToScene.PlaceHolder)
		{
			// The often called tree we will manipulate
			SceneTree tree = useNode.GetTree();

			// Setting the current scene so it can be deleted
			tree.CurrentScene = tree.Root.GetChild(tree.Root.GetChildCount() - 1);

			// Adding the loading box to root
			PackedScene loadingScene;
			loadingScene = (PackedScene)_preloader.GetResource("BackDrop");
			Node loadingScreen = loadingScene.Instantiate();
			tree.Root.CallDeferred("add_child", loadingScreen);

			// Loading up the next area to goto
			PackedScene loadScene = (PackedScene)ResourceLoader.Load(_scenes[_goTo]);

			// Getting rid of the current scene
			tree.CurrentScene.QueueFree();

			// Adding new loaded scene to tree as current scene
			tree.Root.CallDeferred("add_child", loadScene.Instantiate());

			// Getting rid of the static load screen
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
