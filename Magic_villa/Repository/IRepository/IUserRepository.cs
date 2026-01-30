using Magic_villa.Model;
using Magic_villa.Model.Dto;

namespace Magic_villa.Repository.IRepository
{
    public interface IUserRepository 
    {
        Task<bool> IsUniqueUser(string UserName);

        Task<ResponseLoginDto> Login(RequestLoginDto requestLoginDto);
        Task<UserDto> Registration(RequestRgisterationDto requestRgisterationDto);
    }
}
