using Ceramic3D.Controllers;
using Ceramic3D.Views;
using SimpleUi;
using Zenject;

namespace Ceramic3D
{
	namespace Installers
	{
		internal sealed class MainInstaller : MonoInstaller
		{
			public override void InstallBindings()
			{
				Container.BindUiView<MainController, MainView>(GetComponent<MainView>(), transform.parent);
			}
		}
	}
}
