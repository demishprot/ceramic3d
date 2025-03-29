using UnityEngine;

namespace Ceramic3D
{
	namespace Factories
	{
		public interface IPrimitiveFactory
		{
			GameObject Create(Vector3 position, Color color, float size, PrimitiveType type);
		}
	}
}
