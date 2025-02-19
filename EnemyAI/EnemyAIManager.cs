using Godot;
using System;
using System.Collections.Generic;
using BattleSchiffe.Scripts.BoardManager;
public partial class EnemyAIManager : Node
{
	
	[Signal] public delegate void EnemyTurnFinishedEventHandler();

	[Signal] public delegate void AttackEventHandler(int shipCount);
	[Signal] public delegate void SetAttackParametersEventHandler(PlayerBoardManager boardManager);
	
	private List<Ship> currentShips;

	private EnemyBoardManager enemyBoard;
	private PlayerBoardManager playerBoard;

	private int currentFightingForce = 0;
	private int currentBonus = 0;

	private EnemyShipPlacement shipPlacement;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetRequiredDependencies();
		shipPlacement = GetNode<EnemyShipPlacement>("ShipPlacement");
	}
	//Calls all Function needed too generate all the Data the enemy needs for the next match
	private void SetNewMatch()
	{
		UpdateFighhtingForce();
		currentShips = shipPlacement.PlaceShips(currentFightingForce, playerBoard.GetBoard());
		EmitSignal(SignalName.SetAttackParameters, playerBoard);
	}
	
	// Attacks the enemy and switches back to player after the attack is completed
	private void EnemyTurn()
	{
		EmitSignal(SignalName.Attack, currentShips.Count);
		EmitSignal(SignalName.EnemyTurnFinished);
	}
	
	private void UpdateFighhtingForce() 
	{
		int playerFightingForce = 0;
		foreach (Ship ship in playerBoard.GetShips())
		{
			// Get Ship Level and at it to playerFightingForce
		}
	}

	// Function is only for readability
	// ../ is needed to access the Nodes on the same tree height.
	private void SetRequiredDependencies() 
	{
		enemyBoard = GetNode<EnemyBoardManager>("../EnemyBoardManager");
		playerBoard = GetNode<PlayerBoardManager>("../PlayerBoardManager");
	}
	
	// Gets Called in GameManager it only increases it by one bc of the Scaling Function.
	// For more Info Check GDD Enemy AI
	private void UpdateCurrentBonus()
	{
		currentBonus++;
	}

	public void AddShip(Ship ship)
	{
		currentShips.Add(ship);
	}

}
