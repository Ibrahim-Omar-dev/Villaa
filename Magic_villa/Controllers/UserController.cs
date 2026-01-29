using Magic_villa.Model;
using Magic_villa.Model.Dto;
using Magic_villa.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Magic_villa.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ApiResponse apiResponse;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.apiResponse = new ApiResponse();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(RequestLoginDto loginDto)
        {
            var user = await userRepository.Login(loginDto);
            if (user == null || string.IsNullOrEmpty(user.Token))
            {
                apiResponse.StatusCodes=HttpStatusCode.BadRequest;
                apiResponse.IsSuccess=false;
                apiResponse.ErrorMessage="Username or password is incorrect";
                return BadRequest(apiResponse);
            }
            apiResponse.StatusCodes = HttpStatusCode.OK;
            apiResponse.IsSuccess = true;
            apiResponse.Result= user;
            return Ok(apiResponse);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RequestRgisterationDto rgisterationDto)
        {
            bool isUnique = userRepository.IsUniqueUser(rgisterationDto.UserName);
            if (!isUnique)
            {
                apiResponse.StatusCodes = HttpStatusCode.BadRequest;
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = "Username already exists";
                return BadRequest(apiResponse);
            }
            var user = await userRepository.Registration(rgisterationDto);
            if (user == null)
            {
                apiResponse.StatusCodes = HttpStatusCode.BadRequest;
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = "Error While Registerion";
                return BadRequest(apiResponse);
            }
            apiResponse.StatusCodes = HttpStatusCode.OK;
            apiResponse.IsSuccess = true;
            return Ok(apiResponse);
        }
    }
}
