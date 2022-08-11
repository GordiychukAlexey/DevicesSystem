using System.IO;
using DevicesSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.UnityConverters.Math;
using UnityEngine;

public class DataController {
	private const string FilePath = "data.json";
	
	private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings{
			TypeNameHandling = TypeNameHandling.Auto,
			NullValueHandling = NullValueHandling.Ignore,
			Converters = new JsonConverter[]{
				new Vector3Converter(),
			},
		};

	public void Save(DevicesSystemModel devicesSystemModel){
		string jsonToSave = JsonConvert.SerializeObject(devicesSystemModel, Formatting.Indented, _jsonSerializerSettings);

		Debug.Log(jsonToSave);

		WriteString(FilePath, jsonToSave);
	}

	public DevicesSystemModel Load(){
		var loadedJson = ReadString(FilePath);

		DevicesSystemModel loadedModel = JsonConvert.DeserializeObject<DevicesSystemModel>(loadedJson, _jsonSerializerSettings);

		return loadedModel;
	}

	private void WriteString(string path, string str){
		string persistentPath = GetAbsolutePath(path);

		Debug.Log(persistentPath);

		StreamWriter writer = new StreamWriter(persistentPath);
		writer.WriteLine(str);
		writer.Close();
	}

	private string ReadString(string path){
		string persistentPath = GetAbsolutePath(path);

		StreamReader reader = new StreamReader(persistentPath);
		var readed = reader.ReadToEnd();
		reader.Close();

		return readed;
	}

	private string GetAbsolutePath(string localPath) => Path.Combine(Application.persistentDataPath, localPath);
}