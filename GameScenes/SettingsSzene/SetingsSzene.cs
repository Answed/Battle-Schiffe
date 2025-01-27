using Godot;
using System;

public partial class SetingsSzene : Control
{

	public override void _Ready()
	{
	}

	private void OnBackToMainMenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://GameScenes/startSzene/start_menu.tscn");
	}
}
