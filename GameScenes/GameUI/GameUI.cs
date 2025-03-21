using Godot;
using System;

public partial class GameUI : Control
{
	private Texture2D placeholderTexture;
	private ScrollContainer scrollContainer;
	private HBoxContainer hboxContainer;

	public override void _Ready()
	{
		foreach (Node child in GetChildren())
		{
			GD.Print($"Child Node: {child.Name}");
		}
	}

	public Vector2 getGameFieldPosition(){
		return GetNode<Control>("Board").Position;
	}
	public Vector2 getGameFieldSize(){
		return GetNode<Control>("Board").Size;
	}
	public Vector2 getSpawnPosition(){
		BoxContainer SpawnArea = GetNode<BoxContainer>("OwnShips");
		return SpawnArea.Position + (SpawnArea.Size / 2);
	}

	private void openBrakeMenuPressed()
	{
		GetNode<Control>("BrakeMenu").Visible = true;
	}

	private void OnSwitchBoardButtonPressed()
	{
		GD.Print("BOARD CHANGE");
	}
}
