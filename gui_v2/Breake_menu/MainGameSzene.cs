using Godot;
using System;

public partial class MainGameSzene : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
		public void _on_continue_button_break_menu_pressed()
	{
	}
	
	public void _on_restart_game_button_break_menu_pressed()
	{
	}
	
	public void _on_options_button_break_menu_pressed()
	{
	}
	
	public void _on_return_to_main_menu_button_break_menu_pressed()
	{
	}
	
	public void _on_close_game_button_break_menu_pressed()
	{
		GetTree().Quit();
	}
}
