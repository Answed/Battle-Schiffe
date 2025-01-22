using Godot;
using System;

public partial class MainGameEnemySzene : Control
{
	public override void _Ready()
	{
		// Suche den switch_board_button
		var switchBoardButton = GetNode<Button>("interactions/switch_board_button");
		if (switchBoardButton == null)
		{
			GD.PrintErr("switch_board_button nicht gefunden!");
			return;
		}

		// Verbinde das Signal für den Button
		switchBoardButton.Pressed += OnSwitchBoardButtonPressed;
	}

	private void OnSwitchBoardButtonPressed()
	{
		// Szene wechseln
		GD.Print("Switch-Board-Button gedrückt! Wechsel zurück zur MainGameSzene.");
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/mainGameSzene/MainGameSzene.tscn");
	}
}
