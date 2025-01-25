using Godot;
using System;

public partial class BrakeMenuSzene : Control
{
	public override void _Ready()
	{
		// Suche den continue_button
		var continueButton = GetNode<Button>("background/options/continue_button");

		// Suche den close_game_button
		var closeGameButton = GetNode<Button>("background/options/close_game_button");
		
		// Suche return_to_main_menu_button
		var returnToMainMenuButton = GetNode<Button>("background/options/return_to_main_menu_button");

		// Signale für die Buttons verbinden
		continueButton.Pressed += OnContinueButtonPressed;
		returnToMainMenuButton.Pressed += OnReturnToMainMenuButtonPressed;
		closeGameButton.Pressed += OnCloseGameButtonPressed;
	}

	private void OnContinueButtonPressed()
	{
		// Szene wechseln
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/mainGameSzene/MainGameSzene.tscn");
	}
	
	private void OnReturnToMainMenuButtonPressed()
	{
		//Szene wechseln
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/startSzene/start_menu.tscn");
	}
	

	private void OnCloseGameButtonPressed()
	{
		// Spiel beenden
		GD.Print("Close-Game-Button gedrückt! Beende das Spiel.");
		GetTree().Quit();
	}
}
