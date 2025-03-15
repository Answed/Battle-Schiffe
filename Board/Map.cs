
using Godot;
using System;

public partial class Map : Node
{
	[Signal] public delegate void GenerateMapEventHandler(int width, int height);
	
	private void CallGenerateMap(int width, int height)
	
	{
		EmitSignal(SignalName.GenerateMap,20,20);
	}
	
}
