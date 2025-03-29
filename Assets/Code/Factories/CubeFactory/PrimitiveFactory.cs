using UnityEngine;

namespace Ceramic3D
{
	namespace Factories
	{
		public class PrimitiveFactory : IPrimitiveFactory
		{
			public GameObject Create(Vector3 position, Color color, float size, PrimitiveType type)
			{
				GameObject primitive = GameObject.CreatePrimitive(type);
				primitive.transform.position = position;
				primitive.transform.localScale = new Vector3(size, size, size);
				primitive.transform.rotation = Random.rotation;
				primitive.GetComponent<Renderer>().material.color = color;
				return primitive;
			}
		}
	}
}
