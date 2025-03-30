using System.Net.Security;
using System.Runtime.InteropServices;
using Godot;
using static Godot.GD;
public partial class ShipManager : Node2D{
	
	private int HoverCorrection = 0; //before 32 std: 100
	private int SpriteRotation = 0; // 0-3 top->clockwise
	private bool Selected  = false;
	private bool Hovered = false;
	private float MapScale = 1F;
	private Vector2 PlacePosition;
	private ShipControll.ShipType ShipType;

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
			}
			//Hover Sprite
			GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").GlobalPosition = PlacePosition;
		}
		//selects current ship
		if(Hovered && Input.IsActionJustPressed("select"))
		{
			if(Selected)
			{
				Selected = false;
				Position = PlacePosition;
				GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").Visible = false;
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
		Vector2 helper = (Position - nod.Position) / 16 / MapScale; // 16 = map tile size
		helper = new Vector2((int)helper.X,(int)helper.Y);
		PlacePosition = (helper * (200F * Scale)) + nod.Position + (Scale * 100F); 
	}

	public void SetShipType(ShipControll.ShipType type) { ShipType = type; }
	public ShipControll.ShipType GetShipType() { return ShipType; }

	public void LockingShip(){GetNode<Area2D>("Area2D-Collision").Visible = false;}
	public void UnlockingShip(){GetNode<Area2D>("Area2D-Collision").Visible = true;}

	public void HideShip(){Visible = false;}
	public void UnhideShip(){Visible = true;}
	public void DestroyShip(){QueueFree();}
	public void ScaleShip(float Scale){this.Scale = new Vector2(Scale,Scale);}
	public void SetMapScale(float mscale){MapScale = mscale;}
}
