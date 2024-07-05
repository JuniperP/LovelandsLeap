using Godot;
using System;

public partial class ActionRep 
{
	// Each thing that makes up an action
	public string Mapping;
	public InputEvent Input;
	public string Symbol;
	public string ButtonLabel;


	// Constructor to create objects
	public ActionRep(string Mapping, InputEvent Input, string Symbol, string ButtonLabel)
	{
		this.Mapping = Mapping;
		this.Input = Input;
		this.Symbol = Symbol;
		this.ButtonLabel = ButtonLabel;
		
	}


}
