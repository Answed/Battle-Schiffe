using Godot;
using System;
using System.Collections.Generic;
using BattleSchiffe.Scripts.BoardManager;
public partial class EnemyAIManager : Node
{
	private List<Ship> currentShips;

	private EnemyBoardManager enemyBoard;
	private PlayerBoardManager playerBoard;
	private GameManager gameManager; 

	private int currentFightingForce = 0;
	private int currentBonus = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetRequiredDependencies();
	}
	//Calls all Function needed too generate all the Data the enemy needs for the next match
	public void SetNewMatch()
	{
		UpdateFighhtingForce();
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
		gameManager = GetNode<GameManager>("../GameManager");
	}
	
	// Gets Called in GameManager it only increases it by one bc of the Scaling Function.
	// For more Info Check GDD Enemy AI
	public void UpdateCurrentBonus()
	{
		currentBonus++;
	}

	public void AddShip(Ship ship)
	{
		currentShips.Add(ship);
	}
}
