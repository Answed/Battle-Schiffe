using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;


public partial class ShipControll : Node
{
	List<Node2D> ShipList = new List<Node2D>();
	List<ShipType> ShipStorage = new  List<ShipType>();
	Vector2 CreationPosition = new Vector2(500.0F,500.0F);
	enum ShipType{
		TestShip
	}

	[Export] private PackedScene[] Ships;
	public override void _Ready()
	{
		//get correct CreationPostion
		CreateShip(ShipType.TestShip); //placeholder
	}

	public override void _Process(double delta)
	{
	}

	//extra functions
	private void CreateShip(ShipType type){
		switch (type)
		{
			case ShipType.TestShip:
				ShipList.Add((Node2D)Ships[0].Instantiate());
				AddChild(ShipList[ShipList.Count() - 1]);
				ShipList[ShipList.Count() - 1].Position = CreationPosition;
				break;

			default:
				break;
		}
	}
}
