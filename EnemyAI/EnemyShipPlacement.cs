using Godot;
using System;
using System.Collections.Generic;
using BattleSchiffe.Scripts.BoardManager;

public partial class EnemyShipPlacement : Node
{
	[Export]
	private Godot.Collections.Array<ShipBlueprint> shipBlueprints;

	private int[,] board;
	
	public void PlaceShips(int fightForce, int[,] board )
	{
		this.board = board;
		SelectShip(fightForce);
	}

	private void SelectShip(int fightForce)
	{
		Random rand  = new Random();
		while (fightForce != 0)
		{
			int shipLevel = rand.Next(1, 4);

			if (fightForce - shipLevel >= 0)
			{
				Ship ship = new Ship();
				ShipBlueprint shipBlueprint = shipBlueprints[rand.Next(0, shipBlueprints.Count)];
				ship.shipPosition = PlaceSelectedShip(shipBlueprint);
			}
			
		}
	}

	public List<int[,]> PlaceSelectedShip(ShipBlueprint ship)
	{
		bool success = false; // Checks if the Ship can be placed there.
		Random rand = new Random();
		List<int[,]> positions = new List<int[,]>();
		while (success != true)
		{
			int[] startPosition = {rand.Next(0, board.GetLength(0)), rand.Next(0, board.GetLength(1))};
			int[] positionCounter = { 0, 0 }; 
			
			for (int ix = startPosition[0]; ix < board.GetLength(0) + startPosition[0]; ix++)
			{
				for (int iy = startPosition[1]; iy < board.GetLength(1) + startPosition[1]; iy++)
				{
					if (board[ix, iy] == 1)
						success = false;
					if (board[ix, iy] == 0 && ship.position[positionCounter[0]][positionCounter[1]] == 1)
					{
						int[,] shipPosition = new int[,] { { ix, iy } };
						positions.Add(shipPosition);
						positionCounter[0]++;
						positionCounter[1]++;
					}
					else if (board[ix, iy] == 0 && ship.position[positionCounter[0]][positionCounter[1]] == 0)
					{
						positionCounter[0]++;
						positionCounter[1]++;
					}
				}
				success = true;
			}
		}
		return positions;
	}
		
}
