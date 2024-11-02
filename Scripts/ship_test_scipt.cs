using System;
using System.Threading.Tasks;
using Godot;
using Godot.NativeInterop;
using static Godot.GD;

public partial class ship_test_scipt : Node2D{
	public override void _Process(double delta)
	{
		if(((bool)GetMeta("Selected"))){
			//follows mouse
			Position = GetGlobalMousePosition();
			//rotation
			if(Input.IsActionJustPressed("rotate_selected_ship")){
				RotationDegrees += 90;
			}
		}
		//selects current ship
		if((bool)GetMeta("Hovered") && Input.IsActionJustPressed("select")){
			if((bool)GetMeta("Selected")){
				SetMeta("Selected", false);
				Position = (Vector2)GetMeta("SetPosition");
			}
			else{
				SetMeta("Selected", true);
			}
		}
	}
	//checks if the ship is hovered over
	public void MouseEnter(){
		SetMeta("Hovered", true);
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Scale = new Vector2(1.1F,1.1F);
	}
	public void MouseExit(){
		SetMeta("Hovered", false);
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Scale = new Vector2(1.0F,1.0F);
	}
	public void BodyEnter(TileMapLayer nod){
		SetMeta("SetPosition",nod.MapToLocal(nod.LocalToMap(Position)));
		SetMeta("positionViable", true);
		Print(GetMeta("SetPosition")); //for testing
	}
}
