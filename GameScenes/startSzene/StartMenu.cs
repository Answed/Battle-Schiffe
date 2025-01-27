using Godot;

public partial class StartMenu : Control
{

	public override void _Ready()
	{	
	}

	private void OnEndGameButtonPressed()
	{
		// Beendet das Spiel
		GetTree().Quit();
	}
	
	private void OnOpenLoadGameButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://GameScenes/mainGameSzene/MainGameSzene.tscn");
	}
	
	private void OnOpenCreditsButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://GameScenes/startSzene/CreditsSzene.tscn");
	}
	
	private void OnOpenSettingsButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://GameScenes/SettingsSzene/setingsSzene.tscn");
	}
}
