namespace Ceramic3D
{
	namespace Services
	{
		namespace Response
		{
			public interface IBaseResponse
			{
				public string Description { get; set; }
				public EStatusCode StatusCode { get; set; }
			}
		}
	}
}
