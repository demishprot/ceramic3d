using Ceramic3D.Repositories;
using Zenject;

namespace Ceramic3D
{
	namespace Installers
	{
		internal sealed class RepositoriesInstaller : Installer<RepositoriesInstaller>
		{
			public override void InstallBindings()
			{
				Container.BindInterfacesAndSelfTo<DataSaver>().AsSingle();
				Container.BindInterfacesAndSelfTo<MatrixRepository>().AsSingle();
				Container.BindInterfacesAndSelfTo<OffsetRepository>().AsSingle();
			}
		}
	}
}
