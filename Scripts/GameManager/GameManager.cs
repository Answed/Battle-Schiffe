using Godot;
using System;

public partial class GameManager : Node
{
	public override void _Ready()
	{
		GD.Print($"GameManager aktiv. Node-Pfad: {GetPath()}");

		// Suche den settings_button und verbinde sein Signal
		var settingsButton = GetNode<Button>("/root/main_game_szene/settings_button");
		if (settingsButton == null)
		{
			GD.PrintErr("settings_button nicht gefunden!");
		}
		else
		{
			settingsButton.Pressed += OnSettingsButtonPressed;
		}

		// Suche den switch_board_button und verbinde sein Signal
		var switchBoardButton = GetNode<Button>("/root/main_game_szene/switch_board_button");
		if (switchBoardButton == null)
		{
			GD.PrintErr("switch_board_button nicht gefunden!");
		}
		else
		{
			switchBoardButton.Pressed += OnSwitchBoardButtonPressed;
		}
	}

	private void OnSettingsButtonPressed()
	{
		// Szene wechseln
		GD.Print("Settings-Button gedrückt! Wechsel zu den Einstellungen.");
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/brake_menu/brakeMenuSzene.tscn");
	}

	private void OnSwitchBoardButtonPressed()
	{
		// Szene wechseln
		GD.Print("Switch-Board-Button gedrückt! Wechsel zur MainGameEnemySzene.");
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/mainGameSzene/MainGameEnemySzene.tscn");
	}
}
