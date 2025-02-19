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
		this.Visible = false;
	}
	
	private void OnOpenCreditsButtonPressed()
	{
		var scene = GetNode<Node>("res://GameScenes/startSzene/CreditsSzene.tscn"); // Ersetze "PfadZurSzene" mit dem richtigen Node-Pfad
		scene.Visible = true;
	}
	
	private void OnOpenSettingsButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://GameScenes/SettingsSzene/setingsSzene.tscn");
	}
}
