using Godot;
using System;
using BattleSchiffe.Scripts.BoardManager;

public partial class GameManager : Node
{
	[Signal] public delegate void EnemyTurnEventHandler();
	[Signal] public delegate void EnemySetNewMatchEventHandler();
	[Signal] public delegate void EnemyUpdateBonusEventHandler();
	[Signal] public delegate void ShipControllStageBeginEventHandler();
	[Signal] public delegate void ShipControllStageEndEventHandler();
	[Signal] public delegate void ShipControllBoardPlacementFinishedEventHandler();
	
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
		playerBoardManager = GetNode<PlayerBoardManager>("../PlayerBoardManager");
		enemyBoardManager = GetNode<EnemyBoardManager>("../EnemyBoardManager");
	}

	private void UpdateState()
	{
		switch (gameState)
		{
			case GameState.RoundSetup:
				EmitSignal(SignalName.ShipControllStageBegin);
				gameState = GameState.RoundStart;
				UpdateState();
				break;
			case GameState.RoundStart:
				EmitSignal(SignalName.ShipControllBoardPlacementFinished);
				EmitSignal(SignalName.EnemySetNewMatch);
				break;
			case GameState.PlayerTurn:
				EmitSignal(SignalName.EnemyTurn);
				break;	
			case GameState.EnemyTurn:
				break;
			case GameState.RoundEnd:
				EmitSignal(SignalName.ShipControllStageEnd);
				// Cleanup for next Stage
				gameState = GameState.RoundSetup;
				enemyBoardManager.DeleteBoard(); //TODO Create a Signal for that too -> Dont know if we have to Seperate them or use the same Signal for both
				UpdateState();
				break;
		}
	}
	
	public override void _Process(double delta)
	{
		if (currenStage % 5 == 0)
			EmitSignal(SignalName.EnemyUpdateBonus);
	}
	
	public void ChangeToPlayerTurn()
	{
		gameState = GameState.PlayerTurn;
		UpdateState();
	}

	public void PlayerHasWon(bool won)
	{
		//TODO: Management of the win / lose case
		gameState = GameState.RoundEnd;
		UpdateState();
	}

	private void OnEnemyTurnFinished()
	{
		gameState = GameState.PlayerTurn;
		UpdateState();
	}
}
