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
                Url = $"{VillaNumUrl}/VillaNumber",
                Data = villaNumberCreateDto
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.DELETE,
                Url = $"{VillaNumUrl}/VillaNumber/{id}"
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{VillaNumUrl}/VillaNumber"
            });
        }

        public Task<T> GetByIdAsync<T>(int id)
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.GET,
                Url = $"{VillaNumUrl}/VillaNumber/{id}"
            });
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDto villaNumberUpdateDto)
        {
            return SendAsync<T>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Url = $"{VillaNumUrl}/VillaNumber/{villaNumberUpdateDto.VillaNum}",
                Data = villaNumberUpdateDto
            });
        }
    }
}
