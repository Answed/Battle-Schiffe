using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Xml;
using Godot;
using static Godot.GD;
public partial class ShipManager : Node2D
{
	
	private int _spriteRotation = 0; // 0-3 top->clockwise
	private bool _selected  = false;
	private bool _hovered = false;
	private float _mapScale = 1F;
	private List<Vector2I> _boardCords = new();
	private Vector2 _tileMapPosition;
	private Vector2 _placePosition;
	private ShipControll.ShipType _shipType;

	private bool _placed = false;
	
	public override void _Process(double delta)
	{
		if(_selected)
		{
			//follows mouse
			Position = GetGlobalMousePosition();
			//rotation
			if(Input.IsActionJustPressed("rotate_selected_ship"))
			{
				RotationDegrees += 90;
				if(_spriteRotation == 3){ _spriteRotation = 0;}
				else{_spriteRotation++;}

				if(_shipType == ShipControll.ShipType.Corvette || _shipType ==ShipControll.ShipType.Corvette){
					HoverSpriteCorrection();
				}
			}
			//Hover Sprite
			GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").GlobalPosition = _placePosition;
			if(_shipType == ShipControll.ShipType.Corvette || _shipType ==ShipControll.ShipType.Corvette){
				HoverSpriteCorrection();
			}
		}
		//selects current ship
		if(_hovered && Input.IsActionJustPressed("select"))
		{
			if(_selected)
			{
				_selected = false;
				Position = _placePosition;
				GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").Visible = false;
				SetPositions();
				_placed = true; 
			}
			else
			{
				_selected = true;
				GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").Visible = true;
			}
		}
	}
	//checks if the ship is hovered over
	private void MouseEnter()
	{
		_hovered = true;
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Scale = new Vector2(1.1F,1.1F);
	}
	private void MouseExit()
	{
		_hovered = false;
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Scale = new Vector2(1.0F,1.0F);
	}
	private void BodyEnter(TileMapLayer nod) 
	{
		_tileMapPosition = nod.Position;
		Vector2 helper = (Position - nod.Position) / 16 / _mapScale; // 16 = map tile size
		helper = new Vector2((int)helper.X, (int)helper.Y);
		_placePosition = (helper * (200F * Scale)) + nod.Position + (Scale * 100F); 
	}

	public void SetShipType(ShipControll.ShipType type) { _shipType = type; }
	public ShipControll.ShipType GetShipType() { return _shipType; }

	public void LockingShip() { GetNode<Area2D>("Area2D-Collision").Visible = false; }
	public void UnlockingShip() { GetNode<Area2D>("Area2D-Collision").Visible = true; }

	public void HideShip() { Visible = false; }
	public void UnhideShip() { Visible = true; }
	public void DestroyShip() { QueueFree(); }
	public void ScaleShip(float scale) { this.Scale = new Vector2(scale,scale); }
	public void SetMapScale(float mscale) { _mapScale = mscale; }

	private void SetPositions()
	{
		Vector2 helper = (Position - _tileMapPosition) / 16 / _mapScale;
		helper =  new Vector2I((int)helper.X,(int)helper.Y);
		Vector2I helper2 = (Vector2I)helper;
		_boardCords.Add(helper2);
		switch (_shipType)
		{
			case ShipControll.ShipType.Speedboat:
				GD.Print(helper);
				GD.Print(_boardCords);
				break;
			case ShipControll.ShipType.Corvette:
				switch (_spriteRotation)
				{
					case 0:
						helper2 = _boardCords[0];
						helper2.Y = helper2.Y + 1;
						_boardCords.Add(helper2);
						break;
					case 1:
						helper2 = _boardCords[0];
						helper2.X = helper2.X + 1;
						_boardCords.Add(helper2);
						break;
					case 2:
						helper2 = _boardCords[0];
						helper2.Y = helper2.Y - 1;
						_boardCords.Add(helper2);
						break;
					case 3:
						helper2 = _boardCords[0];
						helper2.X = helper2.X - 1;
						_boardCords.Add(helper2);
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
	public List<Vector2I> GetCords() { return _boardCords; }
	public bool IsPlaced() { return _placed; }
	private void HoverSpriteCorrection(){
		switch (_spriteRotation)
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
