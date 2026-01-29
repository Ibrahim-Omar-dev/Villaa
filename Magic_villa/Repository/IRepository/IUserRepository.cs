using Magic_villa.Model;
using Magic_villa.Model.Dto;

namespace Magic_villa.Repository.IRepository
{
    public interface IUserRepository 
    {
        bool IsUniqueUser(string userName);

        Task<ResponseLoginDto> Login(RequestLoginDto requestLoginDto);
        Task<LocalUser> Registration(RequestRgisterationDto requestRgisterationDto);
    }
}
