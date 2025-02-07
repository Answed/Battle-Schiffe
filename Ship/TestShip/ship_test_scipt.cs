using System;
using System.Threading.Tasks;
using Godot;
using Godot.NativeInterop;
using static Godot.GD;

public partial class ship_test_scipt : Node2D{

	Vector2 HoverSpritePosition;
	int SpriteRotation = 0; // 0-3 top->clockwise
	public override void _Process(double delta)
	{
		if((bool)GetMeta("Selected"))
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
		if((bool)GetMeta("Hovered") && Input.IsActionJustPressed("select"))
		{
			if((bool)GetMeta("Selected"))
			{
				SetMeta("Selected", false);
				Position = (Vector2)GetMeta("SetPosition");
				GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").Visible = false;
			}
			else
			{
				SetMeta("Selected", true);
				GetNode<AnimatedSprite2D>("AnimatedSprite2D-Hover").Visible = true;
			}
		}
	}
	//checks if the ship is hovered over
	public void MouseEnter()
	{
		SetMeta("Hovered", true);
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Scale = new Vector2(1.1F,1.1F);
	}
	public void MouseExit()
	{
		SetMeta("Hovered", false);
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Scale = new Vector2(1.0F,1.0F);
	}
	public void BodyEnter(TileMapLayer nod)
	{
		SetMeta("SetPosition",nod.MapToLocal(nod.LocalToMap(Position)));
		HoverSpriteCorection();

		Print(GetMeta("SetPosition")); //for testing
	}


	//extra functions
	void HoverSpriteCorection() 
	{
		HoverSpritePosition = (Vector2)GetMeta("SetPosition");
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
