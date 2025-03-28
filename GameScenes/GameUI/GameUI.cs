using Godot;
using System;

public partial class GameUI : Control
{
	private Texture2D placeholderTexture;
	private ScrollContainer scrollContainer;
	private HBoxContainer hboxContainer;

	private bool playerBoard = true;
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
		//signals for hiding TBI
		if(playerBoard)
		{
			GetNode<Label>("CurrentBoard").Text = "Enemy";
		}
		else
		{
			GetNode<Label>("CurrentBoard").Text = "Player";
		}
		playerBoard =! playerBoard;
	}
}
