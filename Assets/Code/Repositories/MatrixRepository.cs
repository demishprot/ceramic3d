using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Ceramic3D
{
	namespace Repositories
	{
		internal sealed class MatrixRepository : IGetRepository<List<Matrix4x4>>
		{
			private readonly IDataSaver _dataSaver;
			internal MatrixRepository(IDataSaver dataSaver)
			{
				_dataSaver = dataSaver;
			}
			public async UniTask<List<Matrix4x4>> Get(params string[] strings)
			{
				var save = await _dataSaver.LoadDataAsync<List<Matrix4x4>>(Path.Combine(strings));
				return save;
			}
		}
	}
}
