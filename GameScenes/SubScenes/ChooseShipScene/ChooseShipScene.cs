using Godot;
using System;

public partial class ChooseShipScene : Control
{
	// Nodes for the UI elements
	private Label _classOfShip;
	private Label _lifeOfShip;
	private Label _specialAttackShip;
	private Label _notesShip;
	private Button _backToMenuButton;
	private Button _startGameButton;
	
	// TODO Add Ship Struct to easily create new Ships and add them to the selector
	public override void _Ready()
	{
		SetRequiredDependencies();
	}

	private void SetRequiredDependencies()
	{
		// Get references to the nodes
		_classOfShip = GetNode<Label>("stats/stats_GridContainer/class_of_ship");
		_lifeOfShip = GetNode<Label>("stats/stats_GridContainer/life_of_ship");
		_specialAttackShip = GetNode<Label>("stats/stats_GridContainer/special_attack_ship");
		_notesShip = GetNode<Label>("stats/stats_GridContainer/notes_ship");
		_backToMenuButton = GetNode<Button>("back_to_menu_button");
		_startGameButton = GetNode<Button>("Panel3/start_game_button");
	}

	private void OnBackToMenuButtonPressed()
	{
		this.Visible = false;
	}

	private void OnStartGameButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://GameScenes/MasterScene.tscn");
	}
}
