using Godot;
using System;
using System.Collections.Generic;
using BattleSchiffe.Scripts.BoardManager;

enum ShipType
{
	AIRCRAFT_CARRIER,
	AMPHIBIOUS_ASSULT,
	CRUISER,
	DESTROYER,
	CORVETTE,
	SPEEDBOAT
}

private struct ShipBlueprint
{
	//public ShipManager shipManager;
	public ShipType shipType;
}

public partial class EnemyAIManager : Node
{
	private List<Ship> currentShips;

	private EnemyBoardManager enemyBoard;
	private PlayerBoardManager playerBoard;
	//private GameManager gameManager; TODO Add GameManager
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetRequiredDependencies();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void ShipPlacement()
	{
		
	}

	private void SetRequiredDependencies() // Function is only for readability 
	{
		enemyBoard = GetNode<EnemyBoardManager>("EnemyBoardManager");
		playerBoard = GetNode<PlayerBoardManager>("PlayerBoardManager");
		playerBoard = GetNode<PlayerBoardManager>("PlayerBoardManager");
	}
}
