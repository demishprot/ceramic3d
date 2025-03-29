using Ceramic3D.Installers;
using UnityEngine;
using Zenject;

namespace Ceramic3D
{
	internal sealed class ProjectInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Application.targetFrameRate = UnityEngine.Screen.currentResolution.refreshRate;
			Input.multiTouchEnabled = false;

			SignalBusInstaller.Install(Container);
			RepositoriesInstaller.Install(Container);
			MatrixServiceInstaller.Install(Container);
		}
	}
}
