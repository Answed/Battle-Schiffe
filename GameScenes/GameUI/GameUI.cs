using Godot;
using System;

public partial class GameUI : Control
{
	[Signal] public delegate void HidePlayerShipsEventHandler();
	[Signal] public delegate void ShowPlayerShipsEventHandler();

	[Signal] public delegate void MenuOpenMapEventHandler();
	[Signal] public delegate void MenuClosedMapEventHandler();
	[Signal] public delegate void MenuOpenShipsEventHandler();
	[Signal] public delegate void MenuClosedShipsEventHandler();	
	private Texture2D placeholderTexture;
	private ScrollContainer scrollContainer;
	private HBoxContainer hboxContainer;

	private bool playerBoard = true;
	public override void _Ready()
	{
		//foreach (Node child in GetChildren()){GD.Print($"Child Node: {child.Name}");}
	}
	public Vector2 getGameFieldPosition() { return GetNode<Control>("Board").Position; }
	public Vector2 getGameFieldSize() { return GetNode<Control>("Board").Size; }
	public Vector2 getSpawnPosition()
	{
		BoxContainer SpawnArea = GetNode<BoxContainer>("OwnShips");
		return SpawnArea.Position + (SpawnArea.Size / 2);
	}
	private void openBrakeMenuPressed()
	{
		EmitSignal("MenuOpenMap");
		EmitSignal("MenuOpenShips");
		GetNode<Control>("BrakeMenu").Visible = true;
	}
	private void CloseBreakMenu()
	{
		EmitSignal("MenuClosedMap");
		EmitSignal("MenuClosedShips");
	}
	private void OnSwitchBoardButtonPressed()
	{
		//signals for hiding TBI
		if(playerBoard)
		{
			GetNode<Label>("CurrentBoard").Text = "Enemy";
			EmitSignal("HidePlayerShips");
		}
		else
		{
			GetNode<Label>("CurrentBoard").Text = "Player";
			EmitSignal("ShowPlayerShips");
		}
		playerBoard =! playerBoard;
	}
	private void PlacementFinishedPressed()
	{
		//send signal to end board setup
	}
}
