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
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetRequiredDependencies();
		mapGen.GenerateMap(20, 20);
		enemyBoard.InitBoard(mapGen.GetMapGrid(), false, enemyBoard);
		playerBoard.InitBoard(mapGen.GetMapGrid(), true, playerBoard);
		EmitSignal(SignalName.TestShipPlacemenr);

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
}
