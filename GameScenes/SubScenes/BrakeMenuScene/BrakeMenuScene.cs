using Godot;
using System;

public partial class BrakeMenuScene : Control
{
	[Signal] public delegate void BreakMenuCloseEventHandler();

	public override void _Ready(){}

	private void OnContinueButtonPressed()
	{
		this.Visible = false;
		EmitSignal("BreakMenuClose");
	}
	
	private void OnRestartButtonPressed(){
		//GetTree().ChangeSceneToFile("res://GameScenes/UI/start_menu_1.tscn");
		GD.Print("RESART IMPLEMENTIEREN");
	}
	
	private void OnReturnToMainMenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://GameScenes/StartMenu/StartMenu.tscn");
	}
	
	private void OnSettingsButtonPressed()
	{
		GetNode<Control>("Settings").Visible = true;
	}
	

	private void OnCloseGameButtonPressed() { GetTree().Quit(); }
}
