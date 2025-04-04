using Godot;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Text.Json;

public partial class SaveLoadJSONManager<T> : Node
{
	private String _saveLoadFolder;

	public void SetSaveLoadJSONManagerSettings(String saveLoadFolder)
	{
		this._saveLoadFolder = saveLoadFolder;
	}

	public void WriteJSON(T saveLoadObject, String saveLoadFileName)
	{
		string jsonString = JsonSerializer.Serialize<T>(saveLoadObject);
		File.WriteAllText(_saveLoadFolder+saveLoadFileName, jsonString);
		
	}

	public T ReadJSON(String saveLoadFileName)
	{
		string jsonString = File.ReadAllText(_saveLoadFolder+saveLoadFileName);
		return JsonSerializer.Deserialize<T>(jsonString);
	}
}
