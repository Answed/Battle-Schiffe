using Godot;

public partial class StartMenu : Control
{
	// Referenz auf den Button
	private Button _endGameButton;

	public override void _Ready()
	{
		// Hole den Button aus der Szene
		_endGameButton = GetNode<Button>("MarginContainer/Panel/VBoxContainer/end_game_button");

		// Verbinde das Signal mit der Methode
		_endGameButton.Pressed += OnEndGameButtonPressed;
	}

	private void OnEndGameButtonPressed()
	{
		// Beendet das Spiel
		GetTree().Quit();
	}
}
