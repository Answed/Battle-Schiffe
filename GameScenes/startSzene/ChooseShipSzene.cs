using Godot;
using System;
/*
public partial class ChooseShipSzene : Control
{
	// Nodes for the UI elements
	private Label _classOfShip;
	private Label _lifeOfShip;
	private Label _specialAttackShip;
	private Label _notesShip;
	private Button _backToMenuButton;
	private Button _startGameButton;

	// Default values for the ship stats
	private string _defaultClassOfShip = "Dreadnought";
	private int _defaultLifeOfShip = 100;
	private string _defaultSpecialAttackShip = "Laser Barrage";
	private string _defaultNotesShip = "A powerful ship with balanced stats.";

	public override void _Ready()
	{
		// Get references to the nodes
		_classOfShip = GetNode<Label>("stats/stats_GridContainer/class_of_ship");
		_lifeOfShip = GetNode<Label>("stats/stats_GridContainer/life_of_ship");
		_specialAttackShip = GetNode<Label>("stats/stats_GridContainer/special_attack_ship");
		_notesShip = GetNode<Label>("stats/stats_GridContainer/notes_ship");
		_backToMenuButton = GetNode<Button>("back_to_menu_button");
		_startGameButton = GetNode<Button>("start_game_button");

		// Set default values
		_classOfShip.Text = _defaultClassOfShip;
		_lifeOfShip.Text = $"Life: {_defaultLifeOfShip}";
		_specialAttackShip.Text = $"Special Attack: {_defaultSpecialAttackShip}";
		_notesShip.Text = $"Notes: {_defaultNotesShip}";


	private void OnBackToMenuButtonPressed()
	{
		// Load the main menu scene
		GD.Print("Back to Menu Button Pressed");
		GetTree().ChangeSceneToFile("res://GameScenes/startSzene/StartMenu.tscn");
	}

	private void OnStartGameButtonPressed()
	{
		// Load the game scene
		GD.Print("Start Game Button Pressed");
		GetTree().ChangeSceneToFile("res://GameScenes/mainGameSzene/MainGameSzene.tscn");
	}
}
*/
