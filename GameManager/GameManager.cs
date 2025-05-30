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
	[Signal] public delegate void CallGenerateMapEventHandler(int width, int height);
	
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
	
	private PlayerBoardManager _playerBoardManager;
	private EnemyBoardManager _enemyBoardManager;
	
	private GameState _gameState = GameState.RoundSetup;

	public override void _Ready()
	{
		SetRequiredDependencies();
		UpdateState(); //vor test starting game logic
	}

	private void SetRequiredDependencies()
	{
		_playerBoardManager = GetNode<PlayerBoardManager>("../PlayerBoardManager");
		_enemyBoardManager = GetNode<EnemyBoardManager>("../EnemyBoardManager");
	}

	private void UpdateState()
	{
		switch (_gameState)
		{
			case GameState.RoundSetup:
				EmitSignal(SignalName.ShipControllStageBegin);
				EmitSignal(SignalName.CallGenerateMap,10,10);
				_gameState = GameState.RoundStart;
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
				_gameState = GameState.RoundSetup;
				UpdateState();
				break;
		}
	}
	
	public override void _Process(double delta)
	{
		if (currenStage % 5 == 0)
			EmitSignal(SignalName.EnemyUpdateBonus);
	}
	
	private void ChangeToPlayerTurn()
	{
		_gameState = GameState.PlayerTurn;
		UpdateState();
	}

	private void PlayerHasWon(bool won)
	{
		//TODO: Management of the win / lose case
		_gameState = GameState.RoundEnd;
		UpdateState();
	}

	private void OnEnemyTurnFinished()
	{
		_gameState = GameState.PlayerTurn;
		UpdateState();
	}
}
