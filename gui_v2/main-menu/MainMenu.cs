using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_start_game_button_pressed()
	{
	}
	
	public void _on_options_button_main_menu_pressed()
	{
	}
	
	public void _on_open_credits_button_pressed()
	{
	}
	
	public void _on_close_game_button_pressed()
	{
		GetTree().Quit();
	}
}
