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

		// Lade die Platzhalter-Textur
		placeholderTexture = GD.Load<Texture2D>("res://Scenes/GameScenes/mainGameSzene/Texturen/Spielkarten.webp");
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

		// Signal verbinden
		settingsButton.Pressed += OnSettingsButtonPressed;

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
		// Erstelle einen neuen TextureButton
		var button = new TextureButton
		{
			Name = $"Card_{cardId}",
			TextureNormal = placeholderTexture
		};
		button.SetCustomMinimumSize(new Vector2(100, 150));

		// Signal verbinden
		button.Pressed += () => OnCardPressed(cardId);

		// Füge die Karte zur HBoxContainer hinzu
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
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/brake_menu/brakeMenuSzene.tscn");
	}
}
