using Godot;
using System;

public partial class SettingsScene : Control
{

	public override void _Ready()
	{
	}

	private void OnBackToMainMenuButtonPressed()
	{
		this.Visible = false;
	}
}
