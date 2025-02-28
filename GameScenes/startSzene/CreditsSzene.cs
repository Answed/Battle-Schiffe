using Godot;
using System;

public partial class CreditsSzene : Control
{
	public override void _Ready()
	{
	}

	private void OnBackToMainMenuButtonPressed()
	{
		// Szene wechseln
		this.Visible = false;
		var Creditsscene = GetNode<Control>("../CreditsSzene");
		var Settingscene = GetNode<Control>("../SetingsSzene");
		var BrakeScene = GetNode<Control>("../BrakeMenuSzene");
		var ChooseShipScene = GetNode<Control>("../ChooseShipSzene");
		var StartMenuScene = GetNode<Control>("../StartMenu");
		BrakeScene.Visible = false;
		ChooseShipScene.Visible = false;
		Creditsscene.Visible = false;
		Settingscene.Visible = false;
		StartMenuScene.Visible = true;
	}
}
