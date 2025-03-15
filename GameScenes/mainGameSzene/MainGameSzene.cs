using Godot;
using System;

public partial class MainGameSzene : Control
{
	private Texture2D placeholderTexture;
	private ScrollContainer scrollContainer;
	private HBoxContainer hboxContainer;

	public override void _Ready()
	{
		var BrakeScene = GetNode<Control>("BrakeMenu");
		var SettingsScene = GetNode<Control>("Settings");
		BrakeScene.Visible = false;
		SettingsScene.Visible = false;
		
		foreach (Node child in GetChildren())
		{
			GD.Print($"Child Node: {child.Name}");
		}

		// Lade die Platzhalter-Textur
		placeholderTexture = GD.Load<Texture2D>("res://GameScenes/Texturen/Spielkarten.webp");
		GD.Print($"Platzhalter geladen: {placeholderTexture != null}");

		// Initialisiere die Scroll- und HBoxContainer-Referenzen
		scrollContainer = GetNode<ScrollContainer>("CardManager/scrollContainer");
		hboxContainer = GetNode<HBoxContainer>("CardManager/scrollContainer/card_display");

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
		TextureButton button = new TextureButton
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
		float totalWidth = hboxContainer.Size.X;
		float visibleWidth = scrollContainer.GetViewport().GetVisibleRect().Size.X;

		// Scrollen aktivieren, falls nötig
		var hScrollBar = scrollContainer.GetHScrollBar();
		hScrollBar.Visible = totalWidth > visibleWidth;
	}

	private void openBrakeMenuPressed()
	{
		var BrakeScene = GetNode<Control>("BrakeMenu");
		BrakeScene.Visible = true;
	}

	private void OnSwitchBoardButtonPressed()
	{
		GD.Print("Nichts passiert");
	}
	
}
