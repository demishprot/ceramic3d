using Ceramic3D.Extensions;
using Ceramic3D.Repositories;
using Ceramic3D.Services.Response;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ceramic3D
{
	namespace Services
	{
		internal struct SearchModel
		{
			internal List<Vector3> Matched { get; set; }
			internal List<Vector3> Unmatched { get; set; }
		}
		internal struct LoadModel
		{
			internal List<Matrix4x4> Space { get; set; }
			internal List<Matrix4x4> Model { get; set; }
		}
		internal sealed class MatrixService : IMatrixService
		{
			private const string MODEL_FILE_NAME = "model.json";
			private const string SPACE_FILE_NAME = "space.json";
			private const string OUT_FILE_NAME = "out.json";

			private event Action<Vector3, Color> _onSearch;
			public event Action<Vector3, Color> OnSearch { add => _onSearch += value; remove => _onSearch -= value; }

			private readonly ISetRepository<List<SerializableVector3>> _offsetRepository;
			private readonly IGetRepository<List<Matrix4x4>> _matrixRepository;
			public MatrixService(ISetRepository<List<SerializableVector3>> offsetRepository, IGetRepository<List<Matrix4x4>> matrixRepository)
			{
				_offsetRepository = offsetRepository;
				_matrixRepository = matrixRepository;
			}
			public async UniTask<IBaseResponse<LoadModel>> LoadMatrices()
			{
				try
				{
					var model = await _matrixRepository.Get(MODEL_FILE_NAME);
					var space = await _matrixRepository.Get(SPACE_FILE_NAME);
					return new BaseResponse<LoadModel>()
					{
						StatusCode = EStatusCode.OK,
						Data = new()
						{
							Space = space,
							Model = model
						}
					};
				}
				catch (Exception ex)
				{
					return new BaseResponse<LoadModel>()
					{
						Description = $"[LoadMatrices] : {ex.Message}",
						StatusCode = EStatusCode.Failed
					};
				}
			}
			public IBaseResponse<SearchModel> SearchOffsets(List<Matrix4x4> modelMatrices, List<Matrix4x4> spaceMatrices)
			{
				static bool ModelMatchesSpace(Matrix4x4 transformed, Matrix4x4 spaceMatrix)
				{
					for (int i = 0; i < 4; i++)
					{
						for (int j = 0; j < 4; j++)
						{
							if (Mathf.Abs(transformed[i, j] - spaceMatrix[i, j]) > 0.01f)
							{
								return false;
							}
						}
					}
					return true;
				}
				try
				{
					List<Vector3> matched = new List<Vector3>();
					List<Vector3> unmatched = new List<Vector3>();
					foreach (var modelMatrix in modelMatrices)
					{
						Vector3 modelPos = modelMatrix.GetColumn(3);
						bool match = false;
						foreach (var spaceMatrix in spaceMatrices)
						{
							Vector3 spacePosition = spaceMatrix.GetColumn(3);
							Vector3 offset = spacePosition - modelPos;

							Matrix4x4 transformed = Matrix4x4.TRS(offset, Quaternion.identity, Vector3.one) * modelMatrix;

							if (ModelMatchesSpace(transformed, spaceMatrix))
							{
								if (!matched.Contains(offset))
								{
									matched.Add(offset);
									_onSearch(offset, Color.blue);
								}
								match = true;
								break;
							}
						}
						if (!match)
						{
							unmatched.Add(modelPos);
							_onSearch(modelPos, Color.white);
						}
					}
					return new BaseResponse<SearchModel>()
					{
						StatusCode = EStatusCode.OK,
						Data = new()
						{
							Matched = matched,
							Unmatched = unmatched
						}
					};
				}
				catch (Exception ex)
				{
					return new BaseResponse<SearchModel>()
					{
						Description = $"[SearchOffsets] : {ex.Message}",
						StatusCode = EStatusCode.Failed
					};
				}
			}
			public async UniTask<IBaseResponse> UploadOffsets(List<Vector3> matchingOffsets)
			{
				try
				{
					await _offsetRepository.Set(matchingOffsets.Select(offset => offset.ToSerializable()).ToList(), OUT_FILE_NAME);
					return new BaseResponse()
					{
						StatusCode = EStatusCode.OK,
					};
				}
				catch (Exception ex)
				{
					return new BaseResponse()
					{
						Description = $"[UploadOffsets] : {ex.Message}",
						StatusCode = EStatusCode.Failed
					};
				}
			}
		}
	}
}
