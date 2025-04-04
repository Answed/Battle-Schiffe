using Godot;
using System;
using BattleSchiffe.Scripts.BoardManager;
using BattleSchiffe.Scripts.MapGen;

public partial class EnemyAiTest : Node2D
{

	[Signal] public delegate void TestShipPlacementEventHandler();
	[Signal] public delegate void TestShipAttackEventHandler();
	[Signal] public delegate void CallGenerateMapEventHandler(int width, int height);
	
	private EnemyBoardManager _enemyBoard;
	private PlayerBoardManager _playerBoard;
	private EnemyAIManager _enemyAi;
	private MapGen _mapGen;
	private int[,] _copyBoard;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//needs to be called for the setup
		ShipPlacementTest();
		ShipAttackTest();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void SetRequiredDependencies() 
	{
		_mapGen = GetNode<MapGen>("MapGen");
		_enemyBoard = GetNode<EnemyBoardManager>("EnemyBoardManager");
		_playerBoard = GetNode<PlayerBoardManager>("PlayerBoardManager");
		_enemyAi = GetNode<EnemyAIManager>("EnemyAi");
	}
	
	// All Steps needed for testing the Ship Placement in the EnemyAITestScene
	private void ShipPlacementTest()
	{
		SetRequiredDependencies();
		EmitSignal("CallGenerateMap", 20, 20);
		_copyBoard = new int[20, 20];
		CopyBoard(_mapGen.GetMapGrid());
		_enemyBoard.InitBoard(_mapGen.GetMapGrid(), false);
		_playerBoard.InitBoard(_mapGen.GetMapGrid(), true);
		PrintBoard(_mapGen.GetMapGrid());
		EmitSignal(SignalName.TestShipPlacement);
		PrintBoard(_enemyBoard.GetBoard());
		GD.Print(CompareBoard(_copyBoard, _enemyBoard.GetBoard()));
	}

	private void ShipAttackTest()
	{
		EmitSignal(SignalName.TestShipAttack);
		PrintBoard(_playerBoard.GetBoard());
		GD.Print(CountHits(_playerBoard.GetBoard()) == _enemyAi.GetCurrentShipCount());
		PrintBoard(_copyBoard);
	}
	
	private void CopyBoard(int[,] originalBoard)
	{
		for (int i = 0; i < originalBoard.GetLength(0); i++)
		{
			for (int j = 0; j < originalBoard.GetLength(1); j++)
			{
				_copyBoard[i, j] = originalBoard[i, j];
			}
		}	
	}
	public void PrintBoard(int[,] board)
	{
		for (int i = 0; i < board.GetLength(0); i++)
		{
			string line = "";
			for (int j = 0; j < board.GetLength(1); j++)
			{
				line += board[i, j] + " ";
			}
			GD.Print(line);
		}
	}

	private bool CompareBoard(int[,] board1, int[,] board2)
	{
		for (int i = 0; i < board1.GetLength(0); i++)
		{
			for (int j = 0; j < board1.GetLength(1); j++)
			{
				if(board1[i, j] == 1 && board2[i, j] != 1)
					return false;

			}
		}
		return true;
	}

	private int CountHits(int[,] board)
	{
		int hits = 0;
		for (int i = 0; i < board.GetLength(0); i++)
		{
			for (int j = 0; j < board.GetLength(1); j++)
			{
				if(board[i, j] == -1)
					hits++;
			}
		}
		return hits;
	}
}
