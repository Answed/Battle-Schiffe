using Godot;
using System;
using System.Collections.Generic;
using BattleSchiffe.Scripts.BoardManager;

public enum ShipType
{
	AIRCRAFT_CARRIER,
	AMPHIBIOUS_ASSULT,
	CRUISER,
	DESTROYER,
	CORVETTE,
	SPEEDBOAT
}

[GlobalClass]
public partial class ShipBlueprint : Resource
{
	[Export]
	public ShipType shipType { get; set; }
	[Export]
	public Godot.Collections.Array<ShipPositionPrefab> position { get; set; }
}
