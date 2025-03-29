using Ceramic3D.Services;
using Zenject;

namespace Ceramic3D
{
	namespace Installers
	{
		internal sealed class MatrixServiceInstaller : Installer<MatrixServiceInstaller>
		{
			public override void InstallBindings()
			{
				Container.BindInterfacesAndSelfTo<MatrixService>().AsSingle();
			}
		}
	}
}
