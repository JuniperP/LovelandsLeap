public partial class ToggleClassicVerburg : ToggleButton
{
	public static bool classic = false;

	public override void Toggle()
	{
		//👽
	}

	protected override bool GetState()
	{
		return classic;
	}

	protected override void SetState(bool state)
	{
		classic = state;
	}

}
