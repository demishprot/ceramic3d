using Cysharp.Threading.Tasks;

namespace Ceramic3D
{
	namespace Repositories
	{
		internal interface ISetRepository<T>
		{
			UniTask Set(T entity, params string[] strings);
		}
	}
}
