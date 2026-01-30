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
                apiResponse.StatusCodes = HttpStatusCode.BadRequest;
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = "Username or password is incorrect";
                return BadRequest(apiResponse);
            }
            apiResponse.StatusCodes = HttpStatusCode.OK;
            apiResponse.IsSuccess = true;
            apiResponse.Result = user;
            return Ok(apiResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RequestRgisterationDto registrationDto)
        {
            // ✅ FIX 1: AWAIT the async method - this was causing the concurrency error!
            var isUnique = await userRepository.IsUniqueUser(registrationDto.UserName);

            // ✅ FIX 2: Check if NOT unique (false means user already exists)
            if (!isUnique)
            {
                apiResponse.StatusCodes = HttpStatusCode.BadRequest;
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = "Username already exists";
                return BadRequest(apiResponse);
            }

            var user = await userRepository.Registration(registrationDto);
            if (user == null)
            {
                apiResponse.StatusCodes = HttpStatusCode.BadRequest;
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = "Error while registration";
                return BadRequest(apiResponse);
            }

            apiResponse.StatusCodes = HttpStatusCode.OK;
            apiResponse.IsSuccess = true;
            apiResponse.Result = user;
            return Ok(apiResponse);
        }
    }
}