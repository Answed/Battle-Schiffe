using Godot;
using System;

public partial class MainGameSzene : Control
{
	private Texture2D placeholderTexture;
	private ScrollContainer scrollContainer;
	private HBoxContainer hboxContainer;

	public override void _Ready()
	{
		GD.Print($"Aktueller Node: {GetPath()}");
		GD.Print($"Parent: {GetParent().Name}");

		// Prüfe Kinder des aktuellen Nodes
		foreach (Node child in GetChildren())
		{
			GD.Print($"Child Node: {child.Name}");
		}

		// Lade die Platzhalter-Textur
		placeholderTexture = GD.Load<Texture2D>("res://Scenes/GameScenes/Texturen/Spielkarten.webp");
		GD.Print($"Platzhalter geladen: {placeholderTexture != null}");

		// Initialisiere die Scroll- und HBoxContainer-Referenzen
		scrollContainer = GetNode<ScrollContainer>("scrollContainer");
		hboxContainer = GetNode<HBoxContainer>("scrollContainer/card_display");

		// Versuche, den settings_button absolut zu finden
		var settingsButton = GetNode<Button>("/root/main_game_szene/settings_button");
		if (settingsButton == null)
		{
			GD.PrintErr("settings_button nicht gefunden!");
			return;
		}

		// Signal verbinden für settings_button
		settingsButton.Pressed += OnSettingsButtonPressed;

		// Versuche, den switch_board_button zu finden
		var switchBoardButton = GetNode<Button>("/root/main_game_szene/interactions/Switch_board_button");
		if (switchBoardButton == null)
		{
			GD.PrintErr("switch_board_button nicht gefunden!");
			return;
		}

		// Signal verbinden für switch_board_button
		switchBoardButton.Pressed += OnSwitchBoardButtonPressed;

		// Beispiel: Erzeuge 5 Karten
		for (int i = 0; i < 5; i++)
		{
			AddCard(i);
			GD.Print($"Erzeuge Karte {i}");
		}

		UpdateScrollContainer();
	}

	private void AddCard(int cardId)
	{
		var button = new TextureButton
		{
			Name = $"Card_{cardId}",
			TextureNormal = placeholderTexture,
			StretchMode = TextureButton.StretchModeEnum.Scale // Skaliert die Textur
		};

		// Setze die Größe des Buttons auf 50x50
		button.CustomMinimumSize = new Vector2(50, 50);

		// Layout-Flags korrekt setzen
		button.SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter;
		button.SizeFlagsVertical = Control.SizeFlags.ShrinkCenter;

		// Signal verbinden
		button.Pressed += () => OnCardPressed(cardId);

		// Füge den Button zum HBoxContainer hinzu
		hboxContainer.AddChild(button);
	}

	private void OnCardPressed(int cardId)
	{
		GD.Print($"Karte {cardId} wurde angeklickt!");
	}

	private void UpdateScrollContainer()
	{
		var totalWidth = hboxContainer.Size.X;
		var visibleWidth = scrollContainer.GetViewport().GetVisibleRect().Size.X;

		// Scrollen aktivieren, falls nötig
		var hScrollBar = scrollContainer.GetHScrollBar();
		hScrollBar.Visible = totalWidth > visibleWidth;
	}

	private void OnSettingsButtonPressed()
	{
		// Szene wechseln
		GD.Print("Settings-Button gedrückt! Wechsel zu den Einstellungen.");
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/BrakeMenuSzene/brakeMenuSzene.tscn");
	}

	private void OnSwitchBoardButtonPressed()
	{
		// Szene wechseln
		GD.Print("Switch-Board-Button gedrückt! Wechsel zur MainGameEnemySzene.");
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/mainGameSzene/MainGameEnemySzene.tscn");
	}
}
