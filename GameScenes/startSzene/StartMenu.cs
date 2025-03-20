using Godot;

public partial class StartMenu : Control
{

	public override void _Ready()
	{	
		var Credits = GetNode<Control>("Credits");
		var Settingscene = GetNode<Control>("Settings");
		var ChooseShipScene = GetNode<Control>("ShipSelector");
		ChooseShipScene.Visible = false;
		Credits.Visible = false;
		Settingscene.Visible = false;
	}
	
	private void OnOpenShipSelectorButtonPressed()
	{
		var ShipSelector = GetNode<Control>("ShipSelector"); 
		ShipSelector.Visible = true;
	}
	
	private void OnOpenCreditsButtonPressed()
	{
		var Credits = GetNode<Control>("Credits");
		Credits.Visible = true;
	}
	
	private void OnOpenSettingsButtonPressed()
	{
		var Settingscene = GetNode<Control>("Settings");
		Settingscene.Visible = true;
	}
	
		private void CloseGameButtonPressed()
	{
		GetTree().Quit();
	}
}
