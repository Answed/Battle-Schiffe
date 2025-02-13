using Godot;
using static Godot.GD;
public partial class ShipManager : Node2D{

	private int SpriteRotation = 0; // 0-3 top->clockwise
	private bool Selected  = false;
	private bool Hovered = false;
	private Vector2 PlacePosition;
	private Vector2 HoverSpritePosition;

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
				HoverSpriteCorection();
			}
			//Hover Sprite
			GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").GlobalPosition = HoverSpritePosition;
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
		PlacePosition = nod.MapToLocal(nod.LocalToMap(Position));
		HoverSpriteCorection();

		Print(PlacePosition); //for testing
	}

	public void SetShipType(ShipControll.ShipType type){ShipType = type;}
	public ShipControll.ShipType GetShipType(){return ShipType;}

	public void LockingShip(){GetNode<Area2D>("Area2D-Collision").Visible = false;}
	public void UnlockingShip(){GetNode<Area2D>("Area2D-Collision").Visible = true;}
	public void DestroyShip(){QueueFree();}
	//extra functions
	private void HoverSpriteCorection() // Hardcoded Position based on sprite
	{
		HoverSpritePosition = PlacePosition;
		switch (SpriteRotation)
		{
			case 1:
				HoverSpritePosition.X += 32;
				break;
			case 2:
				HoverSpritePosition.Y += 32;
				break;
			case 3:
				HoverSpritePosition.X -= 32;
				break;
			default:
				HoverSpritePosition.Y -= 32;
				break;
		}
	}
}
