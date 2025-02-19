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
		BrakeScene.Visible = false;
		Creditsscene.Visible = true;
	}
	
	private void OnOpenSettingsButtonPressed()
	{
		this.Visible = false;
		var Settingscene = GetNode<Control>("../SetingsSzene");
		var Creditsscene = GetNode<Control>("../CreditsSzene");
		var BrakeScene = GetNode<Control>("../BrakeMenuSzene");
		BrakeScene.Visible = false;
		Settingscene.Visible = true;
	}
}
