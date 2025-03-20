using Godot;
using System;
using BattleSchiffe.Scripts.BoardManager;
using BattleSchiffe.Scripts.MapGen;

public partial class EnemyAiTest : Node2D
{

	[Signal] public delegate void TestShipPlacemenrEventHandler();
	
	private EnemyBoardManager enemyBoard;
	private PlayerBoardManager playerBoard;
	private MapGen mapGen;
	private int[,] copyBoard;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ShipPlacementTest();
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
		EmitSignal(SignalName.TestShipPlacemenr);
		PrintBoard(enemyBoard.GetBoard());
		GD.Print(CompareBoard(copyBoard, enemyBoard.GetBoard()));
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
}
