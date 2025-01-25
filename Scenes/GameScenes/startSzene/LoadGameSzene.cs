using Godot;
using System;

public partial class LoadGameSzene : Control
{

	public override void _Ready()
	{
		// Verbinde den Button mit der Funktion
		var backButton = GetNode<Button>("back_to_main_menu_button");
		backButton.Pressed += OnBackToMainMenuButtonPressed;
	}

	private void OnBackToMainMenuButtonPressed()
	{
		// Szene wechseln
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/startSzene/start_menu.tscn");
	}
}
