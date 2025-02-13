using Godot;
using System;
using System.Collections.Generic;
using BattleSchiffe.Scripts.BoardManager;

public partial class EnemyShipPlacement : Node
{
	[Export]
	private Godot.Collections.Array<ShipBlueprint> shipBlueprints;
	private List<Ship> currentShips;
	private int[,] board;
	
	public List<Ship> PlaceShips(int fightForce, int[,] board )
	{
		this.board = board;
		SelectShip(fightForce);
		return currentShips;
	}

	public void ClearShips() // to Clear Memory
	{
		currentShips.Clear();
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
				if (ship.shipPosition.Count != 0)
				{
					currentShips.Add(ship);
					fightForce -= shipLevel;
				}
			}
		}
	}

	public List<int[,]> PlaceSelectedShip(ShipBlueprint ship)
	{
		bool success = false; // Checks if the Ship can be placed there.
		Random rand = new Random();
		List<int[,]> positions = new List<int[,]>();
		int breakCount = 0;
		while (success != true)
		{
			int[] startPosition = {rand.Next(0, board.GetLength(0)), rand.Next(0, board.GetLength(1))};
			int[] positionCounter = { 0, 0 }; 
			int selectedShipRotation = rand.Next(0, ship.position.Count);
			
			for (int ix = startPosition[0]; ix < board.GetLength(0) + startPosition[0]; ix++)
			{
				for (int iy = startPosition[1]; iy < board.GetLength(1) + startPosition[1]; iy++)
				{
					if (board[ix, iy] == 1)
						goto END; // I hate it too but it is the best way to break out of both loops without needing to call another function or using two if statements and an extra bool
					
					if (ship.position[selectedShipRotation].poitions[positionCounter[0]][positionCounter[1]]== 1)
					{
						int[,] shipPosition = new int[,] { { ix, iy } };
						positions.Add(shipPosition);
						positionCounter[0]++;
						positionCounter[1]++;
					}
					else if (ship.position[selectedShipRotation].poitions[positionCounter[0]][positionCounter[1]]== 1)
					{
						positionCounter[0]++;
						positionCounter[1]++;
					}
				}
				success = true;
			}
			END: ;
			breakCount++;
			
			if(breakCount == 100) // 100 Example Number to prevent infinite loop.
				break;
		}
		return positions;
	}
}
