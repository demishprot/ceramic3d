using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Ceramic3D
{
	namespace Repositories
	{
		[Serializable]
		public struct SerializableVector3
		{
			public float x {  get; set; }
			public float y { get; set; }
			public float z { get; set; }
		}
		internal sealed class OffsetRepository : ISetRepository<List<SerializableVector3>>
		{
			private readonly IDataSaver _dataSaver;
			internal OffsetRepository(IDataSaver dataSaver)
			{
				_dataSaver = dataSaver;
			}
			public async UniTask Set(List<SerializableVector3> entity, params string[] strings)
			{
				await _dataSaver.SaveDataAsync(entity, Path.Combine(strings));
			}
		}
	}
}
