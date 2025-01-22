using Godot;
using System;

public partial class MainGameEnemySzene : Control
{
	private Label numberCvLabel;
	private Label numberCaLabel;
	private Label numberDdLabel;
	private Label numberKLabel;
	private Label numberTbdLabel;

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

		// Suche die Labels
		numberCvLabel = GetNodeOrNull<Label>("background_for_number_of_enemy_ships/GridContainer/number_cv");
		numberCaLabel = GetNodeOrNull<Label>("background_for_number_of_enemy_ships/GridContainer/numer_ca");
		numberDdLabel = GetNodeOrNull<Label>("background_for_number_of_enemy_ships/GridContainer/number_dd");
		numberKLabel = GetNodeOrNull<Label>("background_for_number_of_enemy_ships/GridContainer/number_k");
		numberTbdLabel = GetNodeOrNull<Label>("background_for_number_of_enemy_ships/GridContainer/number_tbd");

		// Initialisiere die Werte
		numberCvLabel.Text = "2"; // Anzahl Flugzeugträger
		numberCaLabel.Text = "0"; // Anzahl Kreuzer
		numberDdLabel.Text = "4"; // Anzahl Zerstörer
		numberKLabel.Text = "3"; // Anzahl Korvetten
		numberTbdLabel.Text = "5"; // Anzahl Torpedoboote
	}

	private void OnSwitchBoardButtonPressed()
	{
		// Szene wechseln
		GD.Print("Switch-Board-Button gedrückt! Wechsel zurück zur MainGameSzene.");
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/mainGameSzene/MainGameSzene.tscn");
	}
}
