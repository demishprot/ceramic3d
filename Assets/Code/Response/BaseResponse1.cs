namespace Ceramic3D
{
	namespace Services
	{
		namespace Response
		{
			public class BaseResponse<T> : IBaseResponse<T>
			{
				public string Description { get; set; }
				public EStatusCode StatusCode { get; set; }
				public T Data { get; set; }
			}
		}
	}
}
