using Ceramic3D.Services.Response;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ceramic3D
{
	namespace Services
	{
		internal interface IMatrixService
		{
			event Action<Vector3, Color> OnMatch;
			UniTask<IBaseResponse<LoadModel>> LoadMatrices();
			IBaseResponse<SearchModel> SearchOffsets(List<Matrix4x4> modelMatrices, List<Matrix4x4> spaceMatrice);
			UniTask<IBaseResponse> UploadOffsets(List<Vector3> matchingOffsets);
		}
	}
}
