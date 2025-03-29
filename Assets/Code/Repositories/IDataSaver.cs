using Cysharp.Threading.Tasks;

namespace Ceramic3D
{
	namespace Repositories
	{
		internal interface IDataSaver
		{
			UniTask SaveDataAsync<T>(T dataToSave, string dataFileName);
			UniTask<T> LoadDataAsync<T>(string dataFileName);
		}
	}
}
