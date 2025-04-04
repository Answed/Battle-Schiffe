using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Xml;
using Godot;
using static Godot.GD;
public partial class ShipManager : Node2D
{
	
	private int SpriteRotation = 0; // 0-3 top->clockwise
	private bool Selected  = false;
	private bool Hovered = false;
	private float MapScale = 1F;
	private List<Vector2I> BoardCords = new();
	private Vector2 TileMapPosition;
	private Vector2 PlacePosition;
	private ShipControll.ShipType ShipType;

	private bool Placed = false;

	public override void _Ready(){}
	
	public override void _Process(double delta)
	{
		if(Selected)
		{
			//follows mouse
			Position = GetGlobalMousePosition();
			//rotation
			if(Input.IsActionJustPressed("rotate_selected_ship"))
			{
				RotationDegrees += 90;
				if(SpriteRotation == 3){ SpriteRotation = 0;}
				else{SpriteRotation++;}

				if(ShipType == ShipControll.ShipType.Corvette || ShipType ==ShipControll.ShipType.Corvette){
					HoverSpriteCorrection();
				}
			}
			//Hover Sprite
			GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").GlobalPosition = PlacePosition;
			if(ShipType == ShipControll.ShipType.Corvette || ShipType ==ShipControll.ShipType.Corvette){
				HoverSpriteCorrection();
			}
		}
		//selects current ship
		if(Hovered && Input.IsActionJustPressed("select"))
		{
			if(Selected)
			{
				Selected = false;
				Position = PlacePosition;
				GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").Visible = false;
				SetPositions();
				Placed = true; 
			}
			else
			{
				Selected = true;
				GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").Visible = true;
			}
		}
	}
	//checks if the ship is hovered over
	public void MouseEnter()
	{
		Hovered = true;
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Scale = new Vector2(1.1F,1.1F);
	}
	public void MouseExit()
	{
		Hovered = false;
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Scale = new Vector2(1.0F,1.0F);
	}
	public void BodyEnter(TileMapLayer nod) 
	{
		TileMapPosition = nod.Position;
		Vector2 helper = (Position - nod.Position) / 16 / MapScale; // 16 = map tile size
		helper = new Vector2((int)helper.X, (int)helper.Y);
		PlacePosition = (helper * (200F * Scale)) + nod.Position + (Scale * 100F); 
	}

	public void SetShipType(ShipControll.ShipType type) { ShipType = type; }
	public ShipControll.ShipType GetShipType() { return ShipType; }

	public void LockingShip() { GetNode<Area2D>("Area2D-Collision").Visible = false; }
	public void UnlockingShip() { GetNode<Area2D>("Area2D-Collision").Visible = true; }

	public void HideShip() { Visible = false; }
	public void UnhideShip() { Visible = true; }
	public void DestroyShip() { QueueFree(); }
	public void ScaleShip(float Scale) { this.Scale = new Vector2(Scale,Scale); }
	public void SetMapScale(float mscale) { MapScale = mscale; }

	private void SetPositions()
	{
		Vector2 helper = (Position - TileMapPosition) / 16 / MapScale;
		helper =  new Vector2I((int)helper.X,(int)helper.Y);
		Vector2I helper2 = (Vector2I)helper;
		BoardCords.Add(helper2);
		switch (ShipType)
		{
			case ShipControll.ShipType.Speedboat:
				GD.Print(helper);
				GD.Print(BoardCords);
				break;
			case ShipControll.ShipType.Corvette:
				switch (SpriteRotation)
				{
					case 0:
						helper2 = BoardCords[0];
						helper2.Y = helper2.Y + 1;
						BoardCords.Add(helper2);
						break;
					case 1:
						helper2 = BoardCords[0];
						helper2.X = helper2.X + 1;
						BoardCords.Add(helper2);
						break;
					case 2:
						helper2 = BoardCords[0];
						helper2.Y = helper2.Y - 1;
						BoardCords.Add(helper2);
						break;
					case 3:
						helper2 = BoardCords[0];
						helper2.X = helper2.X - 1;
						BoardCords.Add(helper2);
						break;
					default:
						break;
				}
				break;
			default:
				GD.Print("Shiptype not implemented for return of position");
				break;
		}
	}
	public List<Vector2I> GetCords() { return BoardCords; }
	public bool IsPlaced() { return Placed; }
	private void HoverSpriteCorrection(){
		switch (SpriteRotation)
		{
			case 0:
				GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").GlobalPosition +=  new Vector2(0, -(100F * Scale.X));
				break;
			case 1:
			 	GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").GlobalPosition +=  new Vector2((100F * Scale.X), 0);
				break;
			case 2:
				GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").GlobalPosition +=  new Vector2(0, (100F * Scale.X));
				break;
			case 3:
			 	GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").GlobalPosition +=  new Vector2(-(100F * Scale.X), 0);
				break;
			default:
				break;
		}
	}
}
