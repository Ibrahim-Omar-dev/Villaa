using Magic_Villa_Web.Models.Dto.VillaDto;

namespace Magic_Villa_Web.Services.IServices
{
    public interface IVillaServices
    {
        Task<T> CreateAsync<T>(VillaCreateDto villaCreateDto);
        Task<T> UpdateAsync<T>(VillaUpdateDto villaUpdateDto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> GetByIdAsync<T>(int id);
        Task<T> GetAllAsync<T>();
    }
}
