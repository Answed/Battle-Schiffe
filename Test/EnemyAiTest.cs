using Godot;
using System;
using BattleSchiffe.Scripts.BoardManager;
using BattleSchiffe.Scripts.MapGen;

public partial class EnemyAiTest : Node2D
{

	[Signal] public delegate void TestShipPlacementEventHandler();
	[Signal] public delegate void TestShipAttackEventHandler();
	
	private EnemyBoardManager enemyBoard;
	private PlayerBoardManager playerBoard;
	private EnemyAIManager enemyAI;
	private MapGen mapGen;
	private int[,] copyBoard;
	
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
		mapGen = GetNode<MapGen>("MapGen");
		enemyBoard = GetNode<EnemyBoardManager>("EnemyBoardManager");
		playerBoard = GetNode<PlayerBoardManager>("PlayerBoardManager");
		enemyAI = GetNode<EnemyAIManager>("EnemyAi");
	}
	
	// All Steps needed for testing the Ship Placement in the EnemyAITestScene
	private void ShipPlacementTest()
	{
		SetRequiredDependencies();
		mapGen.GenerateMap(20, 20);
		copyBoard = new int[20, 20];
		CopyBoard(mapGen.GetMapGrid());
		enemyBoard.InitBoard(mapGen.GetMapGrid(), false, enemyBoard);
		playerBoard.InitBoard(mapGen.GetMapGrid(), true, playerBoard);
		PrintBoard(mapGen.GetMapGrid());
		EmitSignal(SignalName.TestShipPlacement);
		PrintBoard(enemyBoard.GetBoard());
		GD.Print(CompareBoard(copyBoard, enemyBoard.GetBoard()));
	}

	private void ShipAttackTest()
	{
		EmitSignal(SignalName.TestShipAttack);
		PrintBoard(playerBoard.GetBoard());
		GD.Print(CountHits(playerBoard.GetBoard()) == enemyAI.GetCurrentShipCount());
		PrintBoard(copyBoard);
	}
	
	private void CopyBoard(int[,] originalBoard)
	{
		for (int i = 0; i < originalBoard.GetLength(0); i++)
		{
			for (int j = 0; j < originalBoard.GetLength(1); j++)
			{
				copyBoard[i, j] = originalBoard[i, j];
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
