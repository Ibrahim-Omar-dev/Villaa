using Magic_Villa_Web.Model;
using Magic_Villa_Web.Models;

namespace Magic_Villa_Web.Services.IServices
{
    public interface IBaseServices
    {
        public ApiResponse ResponseModel { get; set; }
        public Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
