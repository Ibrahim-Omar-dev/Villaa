using Magic_Villa_Web.Model;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Services.IServices;
using System.Text;
using System.Text.Json;
using static Utility.SD;

//This BaseServices class is a generic HTTP service used to send API requests (GET, POST, PUT, DELETE) to an external API.
//What it does:
//Uses IHttpClientFactory to create an HttpClient
//Builds an HTTP request based on:
//URL
//HTTP method(GET / POST / PUT / DELETE)
//Optional request body (JSON)
//Sends the request to the API
//Reads the response and converts (deserializes) it into a generic type T
//If an error happens, it returns a failed ApiResponse instead of crashing
//It’s a reusable service that handles calling APIs and returning typed responses in one place.

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