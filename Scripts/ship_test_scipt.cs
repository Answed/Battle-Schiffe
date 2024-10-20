using Godot;
//using System;
//using System.Reflection.Metadata;
public partial class ship_test_scipt : Area2D{
	public override void _PhysicsProcess(double delta)
	{
		if(((bool)GetMeta("Selected"))== true && Input.IsActionJustPressed("rotate_selected_ship")){
			RotationDegrees += 90;
		}
	}
}
