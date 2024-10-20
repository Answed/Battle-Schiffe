using Godot;
using System;


public partial class ship_test_scipt : AnimatedSprite2D
{
	
	public override void _PhysicsProcess(double delta)
	{
		if(Input.IsActionJustPressed("rotate_selected_ship")){
			RotationDegrees += 90;
		}
	}
}
