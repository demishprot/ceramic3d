using Ceramic3D.Repositories;
using UnityEngine;

namespace Ceramic3D
{
	namespace Extensions
	{
		public static class Vector3Extensions
		{
			public static SerializableVector3 ToSerializable(this Vector3 vector)
			{
				return new()
				{
					x = vector.x,
					y = vector.y,
					z = vector.z
				};
			}
		}
	}
}
