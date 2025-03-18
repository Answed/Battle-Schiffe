using Godot;
using System;

public partial class CreditsSzene : Control
{
	public override void _Ready()
	{
	}

	private void OnBackToMainMenuButtonPressed()
	{
		this.Visible = false;
	}
}
