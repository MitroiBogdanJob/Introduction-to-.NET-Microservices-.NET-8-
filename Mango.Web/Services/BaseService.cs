using Mango.Web.Models;
using Mango.Web.Services.IService;
using Newtonsoft.Json;
using static Mango.Web.Utility.StaticDetails;

namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClient;
        public BaseService(IHttpClientFactory httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            try
            {
                HttpClient client = _httpClient.CreateClient("MangoApi");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                //token

                message.RequestUri = new Uri(requestDto.ApiUrl);

                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data));
                }

                HttpResponseMessage? apiResponse = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post; break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put; break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete; break;
                    default: message.Method = HttpMethod.Get; break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotExtended:
                        return new() { IsSUcess = false, Message = "Not Found" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { IsSUcess = false, Message = "Acces Denied" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSUcess = false, Message = "Unautorize" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { IsSUcess = false, Message = "Internal Server Error";
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var responseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return responseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSUcess = false
                };
                return dto;
            }

        }
    }
}
