using Godot;

public partial class ActionRep 
{
	// Each thing that makes up an action
	public string Mapping;
	public InputEvent Input;
	public string ButtonLabel;


	// Constructor to create objects
	public ActionRep(string mapping, InputEvent input, string buttonLabel)
	{
		Mapping = mapping;
		Input = input;
		ButtonLabel = buttonLabel;
		
	}


}
