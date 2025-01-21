using Godot;
using System;

public class CardManager : Control
{
	// Anzahl der Karten (kann dynamisch geändert werden)
	private int cardCount = 20;

	// Platzhalter-Textur für die Karten
	private Texture placeholderTexture;

	// Referenzen auf UI-Elemente
	private ScrollContainer scrollContainer;
	private HBoxContainer card_display;

	public override void _Ready()
	{
		// Initialisiere Textur und UI-Referenzen
		placeholderTexture = GD.Load<Texture>("res://Scenes/GameScenes/mainGameSzene/Texturen/background_break_menu.jpg"); // Ersetze durch deinen Platzhalterpfad
		scrollContainer = GetNode<ScrollContainer>("ScrollContainer");
		card_display = GetNode<HBoxContainer>("ScrollContainer/HBoxContainer");

		// Dynamisch Karten hinzufügen
		for (int i = 0; i < cardCount; i++)
		{
			AddCard(i);
		}

		// Update die Scrollbarkeit
		UpdateScrollContainer();
	}

	// Funktion zum Hinzufügen einer Karte
	private void AddCard(int cardId)
	{
		// Erstelle einen neuen Button
		var button = new TextureButton
		{
			Name = $"Card_{cardId}",
			StretchMode = TextureButton.StretchModeEnum.KeepAspect,
			TextureNormal = placeholderTexture,
			RectMinSize = new Vector2(100, 150) // Mindestgröße der Karte
		};

		// Verbinde das Signal für den Klick
		button.Connect("pressed", this, nameof(OnCardPressed), new Godot.Collections.Array { cardId });

		// Füge die Karte zur HBoxContainer hinzu
		hboxContainer.AddChild(button);
	}

	// Signal-Callback, wenn eine Karte angeklickt wird
	private void OnCardPressed(int cardId)
	{
		GD.Print($"Karte {cardId} wurde angeklickt!");
	}

	// Überprüfe und aktiviere die Scrollbarkeit
	private void UpdateScrollContainer()
	{
		// Automatische Scroll-Aktivierung basiert auf der Größe der HBoxContainer
		var totalWidth = hboxContainer.RectSize.x;
		var visibleWidth = scrollContainer.RectSize.x;

		// Aktiviere horizontalen Scrollen, wenn der Inhalt breiter als der Container ist
		scrollContainer.ScrollHorizontalEnabled = totalWidth > visibleWidth;
	}
}
