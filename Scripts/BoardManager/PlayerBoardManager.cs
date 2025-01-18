namespace BattleSchiffe.Scripts.BoardManager;
using System;
using System.Collections.Generic;
using Godot;

public partial class PlayerBoardManager : BoardManager
{
    public void SetShip(List<int[,]> positions, ShipManager shipManager)
    {
        Ship newShip = new Ship();
        newShip.shipPosition = positions;
        newShip.shipManager = shipManager;
        ships.Add(newShip);
    }

    public int GetShipCount()
    {
        return ships.Count;
    }
}