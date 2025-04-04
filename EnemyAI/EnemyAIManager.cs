using Godot;
using System;
using System.Collections.Generic;
using BattleSchiffe.Scripts.BoardManager;
public partial class EnemyAIManager : Node
{
	
	[Signal] public delegate void EnemyTurnFinishedEventHandler();
	[Signal] public delegate void AttackEventHandler(int shipCount);
	[Signal] public delegate void SetAttackParametersEventHandler(PlayerBoardManager boardManager);
	
	private List<Ship> _currentShips;
	private EnemyBoardManager _enemyBoard;
	private PlayerBoardManager _playerBoard;
	private int _currentFightingForce = 10;
	private int _currentBonus = 0;
	private EnemyShipPlacement _shipPlacement;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetRequiredDependencies();
	}
	//Calls all Function needed to generate all the Data the enemy needs for the next match
	private void SetNewMatch()
	{
		//UpdateFighhtingForce();
		_currentShips = _shipPlacement.PlaceShips(_currentFightingForce, _enemyBoard.GetBoard());
		EmitSignal(SignalName.SetAttackParameters, _playerBoard);
		_enemyBoard.SetShips(_currentShips);
	}
	
	// Attacks the enemy and switches back to player after the attack is completed
	private void EnemyTurn()
	{
		EmitSignal(SignalName.Attack, _currentShips.Count);
		EmitSignal(SignalName.EnemyTurnFinished);
	}
	
	private void UpdateFightingForce() 
	{
		int playerFightingForce = 0;
		foreach (Ship ship in _playerBoard.GetShips())
		{
			// Get Ship Level and at it to playerFightingForce
		}
	}

	// Function is only for readability
	// ../ is needed to access the Nodes on the same tree height.
	private void SetRequiredDependencies() 
	{
		_shipPlacement = GetNode<EnemyShipPlacement>("ShipPlacement");
		_enemyBoard = GetNode<EnemyBoardManager>("../EnemyBoardManager");
		_playerBoard = GetNode<PlayerBoardManager>("../PlayerBoardManager");
	}
	
	// Gets Called in GameManager it only increases it by one bc of the Scaling Function.
	// For more Info Check GDD Enemy AI
	private void UpdateCurrentBonus() { _currentBonus++; }
	public int GetCurrentShipCount() { return _currentShips.Count; }
}
