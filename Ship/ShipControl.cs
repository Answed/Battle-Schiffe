using Godot;
using System;


public partial class ShipControl : Node
{
	// Called when the node enters the scene tree for the first time.


	enum ShipType{
		TestShip
	}
	//PackedScene SpawnShip = GD.Load<PackedScene>(export);
	[Export] private PackedScene SpawnShip;
	public override void _Ready()
	{
		Node2D SpawiningShip = (Node2D)SpawnShip.Instantiate();
		AddChild(SpawiningShip);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
