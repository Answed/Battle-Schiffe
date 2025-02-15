using Godot;
using System;
using BattleSchiffe.Scripts.BoardManager;

public partial class GameManager : Node
{
	private enum GameState
	{
		RoundSetup,
		RoundStart,
		RoundEnd,
		PlayerTurn,
		EnemyTurn
	}
	
	public int currenStage = 0;
	public int currentRound = 0;

	private EnemyAIManager enemyAiManager;
	private ShipControll shipControll;
	private PlayerBoardManager playerBoardManager;
	private EnemyBoardManager enemyBoardManager;
	
	private GameState gameState = GameState.RoundStart;

	public override void _Ready()
	{
		SetRequiredDependencies();
	}

	private void SetRequiredDependencies()
	{
		enemyAiManager = GetNode<EnemyAIManager>("../EnemyAi");
		shipControll = GetNode<ShipControll>("../ShipControll");
		playerBoardManager = GetNode<PlayerBoardManager>("../PlayerBoardManager");
		enemyBoardManager = GetNode<EnemyBoardManager>("../EnemyBoardManager");
	}

	private void UpdateState()
	{
		switch (gameState)
		{
			case GameState.RoundSetup:
				shipControll.StageBeginn();
				gameState = GameState.RoundStart;
				UpdateState();
				break;
			case GameState.RoundStart:
				shipControll.BoardPlacementFinished();
				enemyAiManager.SetNewMatch();
				break;
			case GameState.PlayerTurn:
				break;	
			case GameState.EnemyTurn:
				enemyAiManager.EnemyTurn();
				break;
			case GameState.RoundEnd:
				shipControll.StageEnd();
				// Cleanup for next Stage
				gameState = GameState.RoundSetup;
				enemyBoardManager.DeleteBoard();
				UpdateState();
				break;
		}
	}
	
	public override void _Process(double delta)
	{
		if (currenStage % 5 == 0) 
			enemyAiManager.UpdateCurrentBonus();
	}
	
	public void ChangeToPlayerTurn()
	{
		gameState = GameState.PlayerTurn;
		UpdateState();
	}

	public void ChangeToEnemyTurn()
	{
		gameState = GameState.EnemyTurn;
		UpdateState();
	}

	public void PlayerHasWon(bool won)
	{
		//TODO: Management of the win / lose case
		gameState = GameState.RoundEnd;
		UpdateState();
	}
}
