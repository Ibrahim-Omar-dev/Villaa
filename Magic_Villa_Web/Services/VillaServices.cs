using Magic_Villa_Web.Model;
using Magic_Villa_Web.Models.Dto.VillaDto;
using Magic_Villa_Web.Services.IServices;
using static Utility.SD;

namespace Magic_Villa_Web.Services
{
    public class VillaServices : BaseServices, IVillaServices
    {
        public string VillaUrl;
        public VillaServices(IHttpClientFactory httpClientFactory,IConfiguration configuration) : base(httpClientFactory)
        {
            VillaUrl = configuration.GetValue<string>("ServicesUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDto villaCreateDto)
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Data = villaCreateDto,
                Url = VillaUrl + "/api/Villa"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = VillaUrl + "/api/Villa/" + id
            });
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = VillaUrl + "/api/Villa"
            });
        }


        public Task<T> GetByIdAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = VillaUrl + "/api/Villa/" + id
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDto villaUpdateDto)
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Data = villaUpdateDto,
                Url = VillaUrl + "/api/Villa/" + villaUpdateDto.Id
            });
        }
    }
}
