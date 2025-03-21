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
		//GetTree().ChangeSceneToFile("res://GameScenes/UI/start_menu_1.tscn");
		GD.Print("RESART IMPLEMENTIEREN");
	}
	
	private void OnReturnToMainMenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://GameScenes/startSzene/start_menu.tscn");
	}
	
	private void OnSettingsButtonPressed()
	{
		GetNode<Control>("Settings").Visible = true;
	}
	

	private void OnCloseGameButtonPressed()
	{
		GetTree().Quit();
	}
}
