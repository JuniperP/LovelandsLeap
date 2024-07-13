using Godot;

public abstract partial class FadeIn : RichTextLabel
{
	// Boolean to track if process can start fading
	protected bool CanFade;

	// Only emit the signal once
	protected bool Once;

	// Used to ensure previously used inputs can't held to skip fades
	protected bool NotHeld;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CanFade = false;
		Once = true;
		NotHeld = false;
		FurtherSetUp();
	}

	// Starts bringing in logo if told to
	protected void CanNowFade(bool input)
	{
		// Saying fading is now allowed
		CanFade = true;

		// Ensuring same button isn't pressed twice
		NotHeld = input;

		// Start transition with sound effect
		FadeSetUp(input);
	}

	// Allows user to skip the opening fade in 
	public override void _Input(InputEvent OurInput)
	{
		if ((OurInput is InputEventMouseButton || OurInput is InputEventKey) && CanFade && NotHeld)
		{
			NotHeld = false;
			InstantFade();
		}
	}

	// Extra sub class setup
	protected abstract void FurtherSetUp();

	// When fading in is allowed, extra class specific set up is done
	protected abstract void FadeSetUp(bool input);

	// Instantly loads in whatever is fading
	protected abstract void InstantFade();
}
