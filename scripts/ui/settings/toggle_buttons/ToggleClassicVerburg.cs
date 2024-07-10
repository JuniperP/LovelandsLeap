public partial class ToggleClassicVerburg : ToggleButton
{
	public static bool Classic = false;

	public override void Toggle()
	{
		//👽
	}

	protected override bool GetState()
	{
		return Classic;
	}

	protected override void SetState(bool state)
	{
		Classic = state;
	}

}
