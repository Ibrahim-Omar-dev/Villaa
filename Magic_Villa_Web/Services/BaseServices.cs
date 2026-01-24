using Magic_Villa_Web.Model;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Services.IServices;
using System.Text;
using System.Text.Json;
using static Utility.SD;

namespace Magic_Villa_Web.Services
{
    public class BaseServices : IBaseServices
    {
        public ApiResponse ResponseModel { get; set; }
        public IHttpClientFactory HttpClientFactory { get; }

        public BaseServices(IHttpClientFactory httpClientFactory)
        {
            ResponseModel = new();
            HttpClientFactory = httpClientFactory;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = HttpClientFactory.CreateClient("VillaAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(
                        JsonSerializer.Serialize(apiRequest.Data),
                        Encoding.UTF8,
                        "application/json"
                    );
                }

                switch (apiRequest.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var APIResponse = JsonSerializer.Deserialize<T>(apiContent, options);
                return APIResponse!;
            }
            catch (Exception ex)
            {
                var dto = new ApiResponse
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
                var res = JsonSerializer.Serialize(dto);
                var APIResponse = JsonSerializer.Deserialize<T>(res);
                return APIResponse!;
            }
        }
    }
}