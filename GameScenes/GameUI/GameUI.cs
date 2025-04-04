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

	private bool _playerBoard = true;
	public Vector2 GetGameFieldPosition() { return GetNode<Control>("Board").Position; }
	public Vector2 GetGameFieldSize() { return GetNode<Control>("Board").Size; }
	public Vector2 GetSpawnPosition()
	{
		BoxContainer spawnArea = GetNode<BoxContainer>("OwnShips");
		return spawnArea.Position + (spawnArea.Size / 2);
	}
	private void OpenBrakeMenuPressed()
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
		if(_playerBoard)
		{
			GetNode<Label>("CurrentBoard").Text = "Enemy";
			EmitSignal("HidePlayerShips");
		}
		else
		{
			GetNode<Label>("CurrentBoard").Text = "Player";
			EmitSignal("ShowPlayerShips");
		}
		_playerBoard =! _playerBoard;
	}
	private void PlacementFinishedPressed()
	{
		//send signal to end board setup
	}
}
