using SimpleUi.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace Ceramic3D
{
	namespace Views
	{
		internal sealed class MainView : UiView
		{
			[field: SerializeField] public Button StartButton { get; set; }
		}
	}
}
