using Godot;
using System;

public partial class MainGameSzene : Control
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
		return GetNode<Control>("game_field").Position;
	}
	public Vector2 getGameFieldSize(){
		return GetNode<Control>("game_field").Size;
	}
	public Vector2 getSpawnPosition(){
		BoxContainer SpawnArea = GetNode<BoxContainer>("own_ships_display");
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
