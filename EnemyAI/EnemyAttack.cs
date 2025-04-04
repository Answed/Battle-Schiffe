using Godot;
using System;
using BattleSchiffe.Scripts.BoardManager;

public partial class EnemyAttack : Node
{
	private PlayerBoardManager _playerBoardManager;
	private int[,] _board;

	private void SetAttackParameters(PlayerBoardManager board)
	{
		_playerBoardManager = board;
		_board = _playerBoardManager.GetBoard();
	}
	
	private void Attack(int numberOfAttacks)
	{
		Random rand = new Random();
		int xSize = _board.GetLength(0);
		int ySize = _board.GetLength(1);
		GD.Print("x: " + xSize + ", y: " + ySize);
		while (numberOfAttacks > 0)
		{
			int randomX = rand.Next(0, xSize);
			int randomY = rand.Next(0, ySize);
			GD.Print("x: " + randomX + ", y: " + randomY);
			if (Math.Abs(_board[randomX, randomY]) != 1) // Checks if position is a island = 1 or allready attacked = -1
			{
				_playerBoardManager.AttackField(randomX, randomY);
				numberOfAttacks--;
			}
		}
	}
}
