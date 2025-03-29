using Ceramic3D.Factories;
using Ceramic3D.Services;
using Ceramic3D.Services.Response;
using Ceramic3D.Views;
using Cysharp.Threading.Tasks;
using SimpleUi.Abstracts;
using System;
using UnityEngine;
using Zenject;

namespace Ceramic3D
{
	namespace Controllers
	{
		internal sealed class MainController : UiController<MainView>, IInitializable, IDisposable
		{
			internal sealed record MainModel(LoadModel LoadModel);
			private MainModel _model;

			private const float CUBE_SIZE = 2f;

			private IPrimitiveFactory _primitiveFactory;

			private readonly IMatrixService _matrixService;
			private readonly MainView _view;
			public MainController(IMatrixService calculatorService, MainView view)
			{
				_matrixService = calculatorService;
				_view = view;
			}

			public async void Initialize()
			{
				_primitiveFactory = new PrimitiveFactory();
				_matrixService.OnSearch += VisualizeOffset;
				_view.StartButton.onClick.AddListener(VisualizeMatrices);
				await LoadMatrices();
			}
			private async UniTask LoadMatrices()
			{
				var response = await _matrixService.LoadMatrices();
				if (response.StatusCode == EStatusCode.OK)
				{
					_model = new(response.Data);
					_view.gameObject.SetActive(true);
				}
				else
				{
					Debug.LogError(response.Description);
				}
			}
			private async void VisualizeMatrices()
			{
				var searchResponse = _matrixService.SearchOffsets(_model.LoadModel.Model, _model.LoadModel.Space);
				if (searchResponse.StatusCode == EStatusCode.OK)
				{
					var uploadResponse = await _matrixService.UploadOffsets(searchResponse.Data.Matched);
					if (uploadResponse.StatusCode == EStatusCode.Failed)
					{
						Debug.LogError(uploadResponse.Description);
					}
				}
				else
				{
					Debug.LogError(searchResponse.Description);
				}

			}
			private void VisualizeOffset(Vector3 offset, Color color)
			{
				_primitiveFactory.Create(offset, color, CUBE_SIZE, PrimitiveType.Cube);
			}
			public void Dispose()
			{
				_matrixService.OnSearch -= VisualizeOffset;
				_view.StartButton.onClick.RemoveAllListeners();
			}
		}
	}
}