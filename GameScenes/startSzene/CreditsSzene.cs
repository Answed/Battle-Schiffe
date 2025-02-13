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
		GetTree().ChangeSceneToFile("res://GameScenes/startSzene/start_menu.tscn");
	}
}
