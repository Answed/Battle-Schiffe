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
		GetTree().ChangeSceneToFile("res://GameScenes/mainGameSzene/MainGameSzene.tscn");
	}
	
	private void OnReturnToMainMenuButtonPressed()
	{
		//Szene wechseln
		GetTree().ChangeSceneToFile("res://GameScenes/startSzene/start_menu.tscn");
	}
	

	private void OnCloseGameButtonPressed()
	{
		// Spiel beenden
		GetTree().Quit();
	}
}
