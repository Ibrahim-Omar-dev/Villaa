using Magic_Villa_Web.Models.Dto.VillaNumberDto;

namespace Magic_Villa_Web.Services.IServices
{
    public interface IVillaNumberServices
    {
        Task<T> CreateAsync<T>(VillaNumberCreateDto villaNumberCreateDto);
        Task<T> UpdateAsync<T>(VillaNumberUpdateDto villaNumberUpdateDto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> GetByIdAsync<T>(int id);
        Task<T> GetAllAsync<T>();
    }
}
