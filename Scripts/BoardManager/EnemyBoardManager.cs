namespace BattleSchiffe.Scripts.BoardManager;
using System;
using System.Collections.Generic;
using Godot;


public partial class EnemyBoardManager : BoardManager 
{
		public void SetShips(List<Ship> ships)
		{
				this.ships = ships;
		}
}
