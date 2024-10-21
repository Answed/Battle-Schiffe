using System;
using System.Threading.Tasks;
using Godot;
using Godot.NativeInterop;

public partial class ship_test_scipt : Node2D{
	public override void _Process(double delta)
	{
		if(((bool)GetMeta("Selected"))== true ){
			//follows mouse
			Position = GetGlobalMousePosition();
			//rotation
			if( Input.IsActionJustPressed("rotate_selected_ship")){
				RotationDegrees += 90;
			}
		}
		//selects current ship
		if((bool)GetMeta("Hovered") == true && Input.IsActionJustPressed("select")){
			if((bool)GetMeta("Selected") == true){
				SetMeta("Selected", false);
			}
			else{
				SetMeta("Selected", true);
			}
		}
	}
	//checks if the ship is hovered over
	public void _on_area_2d_mouse_entered(){
		SetMeta("Hovered", true);
		GetNode<Area2D>("Area2D").GetNode<AnimatedSprite2D>("AnimatedSprite2D").Scale = new Vector2(1.1F,1.1F);
	}
	public void _on_area_2d_mouse_exited(){
		SetMeta("Hovered", false);
		GetNode<Area2D>("Area2D").GetNode<AnimatedSprite2D>("AnimatedSprite2D").Scale = new Vector2(1.0F,1.0F);
	}
}
