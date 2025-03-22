using Godot;
using System;
using BattleSchiffe.Scripts.BoardManager;

public partial class EnemyAttack : Node
{
	private PlayerBoardManager playerBoard;
	private int[,] boardArray;

	private void SetAttackParameters(PlayerBoardManager board)
	{
		playerBoard = board;
		boardArray = playerBoard.GetBoard();
	}
	
	private void Attack(int numberOfAttacks)
	{
		Random rand = new Random();
		int xSize = boardArray.GetLength(0);
		int ySize = boardArray.GetLength(1);
		GD.Print("x: " + xSize + ", y: " + ySize);
		while (numberOfAttacks > 0)
		{
			int randomX = rand.Next(0, xSize);
			int randomY = rand.Next(0, ySize);
			GD.Print("x: " + randomX + ", y: " + randomY);
			if (Math.Abs(boardArray[randomX, randomY]) != 1) // Checks if position is a island = 1 or allready attacked = -1
			{
				playerBoard.AttackField(randomX, randomY);
				numberOfAttacks--;
			}
		}
	}
}
