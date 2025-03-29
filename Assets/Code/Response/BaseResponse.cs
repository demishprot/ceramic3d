namespace Ceramic3D
{
	namespace Services
	{
		namespace Response
		{
			public class BaseResponse : IBaseResponse
			{
				public string Description { get; set; }
				public EStatusCode StatusCode { get; set; }
			}
		}
	}
}
