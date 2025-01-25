using Godot;
using System;

public partial class SetingsSzene : Control
{

	public override void _Ready()
	{
		// Verbinde den Button mit der Funktion
		var backButton = GetNode<Button>("back_to_main_menu_button");
		backButton.Pressed += OnBackToMainMenuButtonPressed;
	}

	private void OnBackToMainMenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/startSzene/start_menu.tscn");
	}
}
