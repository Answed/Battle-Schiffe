using Godot;
using System;

public partial class BrakeMenuSzene : Control
{
	public override void _Ready()
	{
	}

	private void OnContinueButtonPressed()
	{
		// Szene wechseln
		var BrakeScene = GetNode<Control>("../BrakeMenuSzene");
		BrakeScene.Visible = false;
	}
	
	private void OnReturnToMainMenuButtonPressed()
	{
		//Szene wechseln
		this.Visible = false;
		var StartMenuScene = GetNode<Control>("../StartMenu");
		StartMenuScene.Visible = true;
	}
	

	private void OnCloseGameButtonPressed()
	{
		// Spiel beenden
		GetTree().Quit();
	}
}
