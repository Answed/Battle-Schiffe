using Godot;
using System;
using BattleSchiffe.Scripts.BoardManager;

public partial class EnemyAttack : Node
{
	private PlayerBoardManager playerBoard;
	private int[,] boardArray;

	public void SetAttackParameters(PlayerBoardManager board)
	{
		playerBoard = board;
		boardArray = playerBoard.GetBoard();
	}

	public void Attack(int numberOfAttacks)
	{
		Random rand = new Random();
		int xSize = boardArray.GetLength(0);
		int ySize = boardArray.GetLength(1);
		while (numberOfAttacks > 0)
		{
			int randomX = rand.Next(0, xSize);
			int randomY = rand.Next(0, ySize);
			if (boardArray[randomX, randomY] != 1)
			{
				playerBoard.AttackField(randomX, randomY);
				numberOfAttacks--;
			}
		}
	}
}
