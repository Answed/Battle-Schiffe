using Godot;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Text.Json;

public partial class SaveLoadJSONManager<T> : Node
{
	private String saveLoadFolder;

	public void SetSaveLoadJSONManagerSettings(String saveLoadFolder)
	{
		this.saveLoadFolder = saveLoadFolder;
	}

	public void WriteJSON(T saveLoadObject, String saveLoadFileName)
	{
		string jsonString = JsonSerializer.Serialize<T>(saveLoadObject);
		File.WriteAllText(saveLoadFolder+saveLoadFileName, jsonString);
		
	}

	public T ReadJSON(String saveLoadFileName)
	{
		string jsonString = File.ReadAllText(saveLoadFolder+saveLoadFileName);
		return JsonSerializer.Deserialize<T>(jsonString);
	}
}
