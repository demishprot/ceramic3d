using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Ceramic3D
{
	namespace Repositories
	{
		internal sealed class DataSaver : IDataSaver
		{
			public async UniTask SaveDataAsync<T>(T dataToSave, string dataFileName)
			{
				string tempPath = Application.streamingAssetsPath;
				tempPath = Path.Combine(tempPath, dataFileName);

				JsonConvert.DefaultSettings = () => new JsonSerializerSettings();
				string jsonData = JsonConvert.SerializeObject(dataToSave, Formatting.Indented, new JsonSerializerSettings
				{
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				});
				byte[] jsonByte = Encoding.UTF8.GetBytes(jsonData);


				if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
				}

				try
				{
					await File.WriteAllBytesAsync(tempPath, jsonByte).AsUniTask();
					Debug.Log("Saved Data to: " + tempPath.Replace("/", "\\"));
				}
				catch (Exception e)
				{
					Debug.LogWarning("Failed Save Data to: " + tempPath.Replace("/", "\\"));
					Debug.LogWarning("Error: " + e.Message);
				}
			}
			public async UniTask<T> LoadDataAsync<T>(string dataFileName)
			{
				string tempPath = Application.streamingAssetsPath;
				tempPath = Path.Combine(tempPath, dataFileName);

				if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
				{
					Debug.LogWarning("Directory does not exist");
					return default(T);
				}

				if (!File.Exists(tempPath))
				{
					Debug.Log("File does not exist");
					return default(T);
				}

				byte[] jsonByte = null;
				try
				{
					jsonByte = await File.ReadAllBytesAsync(tempPath).AsUniTask();
					Debug.Log("Loaded Data from: " + tempPath.Replace("/", "\\"));
				}
				catch (Exception e)
				{
					Debug.LogWarning("Failed To Load Data from: " + tempPath.Replace("/", "\\"));
					Debug.LogWarning("Error: " + e.Message);
				}

				string jsonData = Encoding.UTF8.GetString(jsonByte);
				return JsonConvert.DeserializeObject<T>(jsonData);
			}
		}
	}
}
