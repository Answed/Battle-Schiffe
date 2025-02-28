using Godot;

public partial class StartMenu : Control
{

	public override void _Ready()
	{	
	}

	private void OnEndGameButtonPressed()
	{
		// Beendet das Spiel
		GetTree().Quit();
	}
	
	private void OnOpenLoadGameButtonPressed()
	{
		this.Visible = false;
		var scene = GetNode<Control>("../ChooseShipSzene"); 
		scene.Visible = true;
	}
	
	private void OnOpenCreditsButtonPressed()
	{
		this.Visible = false;
		var Creditsscene = GetNode<Control>("../CreditsSzene");
		var Settingscene = GetNode<Control>("../SetingsSzene");
		var BrakeScene = GetNode<Control>("../BrakeMenuSzene");
		var ChooseShipScene = GetNode<Control>("../ChooseShipSzene");
		var StartMenuScene = GetNode<Control>("../StartMenu");
		BrakeScene.Visible = false;
		StartMenuScene.Visible = false;
		ChooseShipScene.Visible = false;
		Settingscene.Visible = false;
		Creditsscene.Visible = true;
	}
	
	private void OnOpenSettingsButtonPressed()
	{
		this.Visible = false;
		var Creditsscene = GetNode<Control>("../CreditsSzene");
		var Settingscene = GetNode<Control>("../SetingsSzene");
		var BrakeScene = GetNode<Control>("../BrakeMenuSzene");
		var ChooseShipScene = GetNode<Control>("../ChooseShipSzene");
		var StartMenuScene = GetNode<Control>("../StartMenu");
		BrakeScene.Visible = false;
		StartMenuScene.Visible = false;
		ChooseShipScene.Visible = false;
		Creditsscene.Visible = false;
		Settingscene.Visible = true;
	}
}
