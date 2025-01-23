using Godot;
using System;

public partial class BrakeMenuSzene : Control
{
	public override void _Ready()
	{
		// Suche den continue_button
		var continueButton = GetNode<Button>("background/options/continue_button");
		if (continueButton == null)
		{
			GD.PrintErr("continue_button nicht gefunden!");
			return;
		}

		// Suche den close_game_button
		var closeGameButton = GetNode<Button>("background/options/close_game_button");
		if (closeGameButton == null)
		{
			GD.PrintErr("close_game_button nicht gefunden!");
			return;
		}


		// Signale für die Buttons verbinden
		continueButton.Pressed += OnContinueButtonPressed;
		closeGameButton.Pressed += OnCloseGameButtonPressed;
	}

	private void OnContinueButtonPressed()
	{
		// Szene wechseln
		GD.Print("Continue-Button gedrückt! Wechsel zurück zur MainGameSzene.");
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/mainGameSzene/MainGameSzene.tscn");
	}

	private void OnCloseGameButtonPressed()
	{
		// Spiel beenden
		GD.Print("Close-Game-Button gedrückt! Beende das Spiel.");
		GetTree().Quit();
	}
}
