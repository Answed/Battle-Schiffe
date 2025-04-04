using Godot;

public partial class StartMenu : Control
{

	public override void _Ready(){}
	private void OnOpenShipSelectorButtonPressed()
	{
		GetNode<Control>("ShipSelector").Visible = true;
	}
	private void OnOpenSettingsButtonPressed()
	{
		GetNode<Control>("Settings").Visible = true;
	}
	private void OnOpenCreditsButtonPressed()
	{
		GetNode<Control>("Credits").Visible = true;
	}
	private void CloseGameButtonPressed() { GetTree().Quit(); }
}
