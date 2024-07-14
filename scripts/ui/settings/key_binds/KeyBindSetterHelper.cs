using Godot;


public partial class KeyBindSetterHelper : Node
{

	// Sets the key bind for an action in the game given a action to replace and user input
	public static void SetKeyBind(InputEvent ourInput, UserAction ourAction)
	{

		// Gets rid of all other key binds
		InputMap.ActionEraseEvents(Keybinds._acts[ourAction].Mapping);

		// Updates current stored key to click for that action's key bind
		Keybinds._acts[ourAction].Input = ourInput;

		// Maps wanted button
		InputMap.ActionAddEvent(Keybinds._acts[ourAction].Mapping, Keybinds._acts[ourAction].Input);
	}
}
