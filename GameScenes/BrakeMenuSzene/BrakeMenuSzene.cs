using Godot;
using System;

public partial class BrakeMenuSzene : Control
{
	public override void _Ready()
	{
	}

	private void OnContinueButtonPressed()
	{
		this.Visible = false;
	}
	
	private void OnRestartButtonPressed(){
		GetTree().ChangeSceneToFile("res://GameScenes/UI/start_menu_1.tscn");
	}
	
	private void OnReturnToMainMenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://GameScenes/UI/start_menu_1.tscn");
	}
	
	private void OnSettingsButtonPressed()
	{
		var SettingsScene = GetNode<Control>("../Settings");
		SettingsScene.Visible = true;
	}
	

	private void OnCloseGameButtonPressed()
	{
		GetTree().Quit();
	}
}
