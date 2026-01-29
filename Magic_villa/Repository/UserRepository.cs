using Magic_villa.Data;
using Magic_villa.Model;
using Magic_villa.Model.Dto;
using Magic_villa.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Magic_villa.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly string _secret;

        public UserRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            _secret = configuration.GetValue<string>("Token:Key");
        }

        public bool IsUniqueUser(string userName)
        {
            var user = appDbContext.LocalUsers.FirstOrDefault(u => u.UserName == userName);
            return user == null;
        }

        public async Task<ResponseLoginDto> Login(RequestLoginDto requestLoginDto)
        {
            var user = appDbContext.LocalUsers
                .FirstOrDefault(u => u.UserName.ToLower() == requestLoginDto.UserName.ToLower()
                && u.Password == requestLoginDto.Password);

            if (user == null)
            {
                return new ResponseLoginDto
                {
                    Token = "",
                    LocalUser = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new ResponseLoginDto
            {
                Token = tokenHandler.WriteToken(token),
                LocalUser = user
            };
        }

        public async Task<LocalUser> Registration(RequestRgisterationDto requestRegistrationDto) 
        {
            LocalUser user = new()
            {
                UserName = requestRegistrationDto.UserName,
                Name = requestRegistrationDto.Name,
                Password = requestRegistrationDto.Password,
                Role = requestRegistrationDto.Role
            };

            await appDbContext.LocalUsers.AddAsync(user);
            await appDbContext.SaveChangesAsync();

            user.Password = "";
            return user;
        }
    }
}