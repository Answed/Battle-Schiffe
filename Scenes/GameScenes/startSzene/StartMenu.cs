using Godot;

public partial class StartMenu : Control
{
	// Referenz auf den Button
	private Button _endGameButton;
	private Button _openLoadGameButton;
	private Button _openSettingsButton;

	public override void _Ready()
	{
		// Hole den Button aus der Szene
		_endGameButton = GetNode<Button>("MarginContainer/Panel/VBoxContainer/end_game_button");
		_openLoadGameButton = GetNode<Button>("MarginContainer/Panel/VBoxContainer/open_load_game_button");
		_openSettingsButton = GetNode<Button>("MarginContainer/Panel/VBoxContainer/open_settings_button");

		// Verbinde das Signal mit der Methode
		_endGameButton.Pressed += OnEndGameButtonPressed;
		_openLoadGameButton.Pressed += OnOpenLoadGameButtonPressed;
		_openSettingsButton.Pressed += OnOpenSettingsButtonPressed;
	}

	private void OnEndGameButtonPressed()
	{
		// Beendet das Spiel
		GetTree().Quit();
	}
	
	private void OnOpenLoadGameButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/startSzene/LoadGameSzene.tscn");
	}
	
	private void OnOpenSettingsButtonPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/GameScenes/SettingsSzene/setingsSzene.tscn");
	}
}
