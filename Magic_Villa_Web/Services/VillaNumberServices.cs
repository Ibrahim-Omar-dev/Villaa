using Magic_Villa_Web.Model;
using Magic_Villa_Web.Models.Dto.VillaNumberDto;
using Magic_Villa_Web.Services.IServices;
using static Utility.SD;

namespace Magic_Villa_Web.Services
{
    public class VillaNumberServices : BaseServices, IVillaNumberServices
    {
        public string VillaNumUrl;
        public VillaNumberServices(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            VillaNumUrl = configuration.GetValue<string>("ServicesUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaNumberCreateDto villaNumberCreateDto)
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.POST,
                Url = $"{VillaNumUrl}/api/VillaNumber",
                Data = villaNumberCreateDto
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"{VillaNumUrl}/api/VillaNumber/{id}"
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{VillaNumUrl}/api/VillaNumber"
            });
        }

        public Task<T> GetByIdAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{VillaNumUrl}/api/VillaNumber/{id}"
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDto villaNumberUpdateDto)
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Url = $"{VillaNumUrl}/api/VillaNumber/{villaNumberUpdateDto.VillaNum}",
                Data = villaNumberUpdateDto
            });
        }
    }
}
