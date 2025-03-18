using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using BattleSchiffe.Scripts.BoardManager;

public partial class EnemyShipPlacement : Node
{
	[Export]
	private Godot.Collections.Array<ShipBlueprint> shipBlueprints;
	private List<Ship> currentShips;
	private int[,] board;

	public override void _Ready()
	{
		currentShips = new List<Ship>();
	}

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
					GD.Print(shipBlueprint.shipType);
					currentShips.Add(ship);
					fightForce -= shipLevel;
					GD.Print(fightForce);
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
			int selectedShipRotation = rand.Next(0, ship.position.Count);
			int maxXValue = ship.position[selectedShipRotation].positions.Count;
			int maxYValue = ship.position[selectedShipRotation].positions[0].GetLength(0);
			int[] startPosition = {rand.Next(0, board.GetLength(0)- maxXValue - 1), rand.Next(0, board.GetLength(1) - maxYValue - 1)};
			int[] currentPosition = new int[2] { startPosition[0], startPosition[1] };

			bool shipFits = true;
			
			for (int ix = 0; ix < maxXValue; ix++)
			{
				for (int iy = 0; iy < maxYValue; iy++)
				{
					if (board[currentPosition[0], currentPosition[1]] != 0)
					{
						shipFits = false;
						ResetBoard(positions);
						positions.Clear();
						break;
					}
					
					if (ship.position[selectedShipRotation].positions[ix][iy] == 1)
					{
						int[,] shipPosition = { { currentPosition[0], currentPosition[1] } };
						positions.Add(shipPosition);
						board[currentPosition[0], currentPosition[1]] = 2;
					}
					currentPosition[1]++;
				}
				if (shipFits == false)
					break;
				currentPosition[1] = startPosition[1];
				currentPosition[0]++;
			}
			breakCount++;
			
			if(breakCount == 100) // 100 Example Number to prevent infinite loop.
				break;
			if(shipFits)
				return positions;
		}
		return null;
	}
	
	// Board sets position where a ship is to 2 but the change gets applied after all ships have been placed
	// To prevent this the positions gets temporaly set and resettet when a ship does not fit.
	private void ResetBoard(List<int[,]> shipPositions)
	{
		foreach (var position in shipPositions)
		{
			board[position[0, 0], position[0, 1]] = 0;
		}
	}
}
