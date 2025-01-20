using Godot;
using System;

public partial class MainGameSzene : Control
{
	public void _on_open_break_menu_button_pressed()
	{
		// Szene laden
		PackedScene newScene = ResourceLoader.Load<PackedScene>("res://Breake_menu/main_game_szene.tscn");
		if (newScene == null)
		{
			GD.PrintErr("Die Szene konnte nicht geladen werden. Überprüfen Sie den Pfad!");
			return;
		}

		// Instanz der neuen Szene erstellen
		var instance = newScene.Instantiate();
		if (instance == null)
		{
			GD.PrintErr("Instanzierung der Szene fehlgeschlagen. Überprüfen Sie das Script.");
			return;
		}

		// Neue Szene hinzufügen
		GetTree().Root.AddChild(instance);

		// Aktuelle Szene entfernen
		var currentScene = GetTree().CurrentScene;
		currentScene.QueueFree();

		// Neue Szene als aktiv setzen
		GetTree().CurrentScene = instance;
	}
}
