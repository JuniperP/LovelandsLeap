using Godot;

public partial class ActionRep 
{
	// Each thing that makes up an action
	public string Mapping;
	public InputEvent Input;
	public string ButtonLabel;


	// Constructor to create objects
	public ActionRep(string Mapping, InputEvent Input, string ButtonLabel)
	{
		this.Mapping = Mapping;
		this.Input = Input;
		this.ButtonLabel = ButtonLabel;
		
	}


}
