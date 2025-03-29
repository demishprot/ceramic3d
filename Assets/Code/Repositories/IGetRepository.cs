using Cysharp.Threading.Tasks;

namespace Ceramic3D
{
	namespace Repositories
	{
		internal interface IGetRepository<T>
		{
			UniTask<T> Get(params string[] strings);
		}
	}
}
