using AutoMapper;
using Magic_villa.Model;
using Magic_villa.Model.Dto;
using Magic_villa.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Magic_villa.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly string _secret;

        // ✅ Removed AppDbContext - it's not used anywhere
        public UserRepository(IConfiguration configuration,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            _secret = configuration.GetValue<string>("Token:Key")
                ?? throw new ArgumentNullException(nameof(configuration), "Token:Key configuration is missing");
        }

        public async Task<bool> IsUniqueUser(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return user == null;
        }

        public async Task<ResponseLoginDto> Login(RequestLoginDto requestLoginDto)
        {
            var user = await userManager.FindByNameAsync(requestLoginDto.UserName);

            if (user == null)
            {
                return new ResponseLoginDto
                {
                    Token = "",
                    User = null
                };
            }

            bool isValid = await userManager.CheckPasswordAsync(user, requestLoginDto.Password);
            if (!isValid)
            {
                return new ResponseLoginDto
                {
                    Token = "",
                    User = null
                };
            }

            var roles = await userManager.GetRolesAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new ResponseLoginDto
            {
                Token = tokenHandler.WriteToken(token),
                User = mapper.Map<UserDto>(user)
            };
        }

        public async Task<UserDto> Registration(RequestRgisterationDto dto)
        {
            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Name = dto.Name
            };

            var result = await userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ",
                    result.Errors.Select(e => e.Description)));
            }

            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
                await roleManager.CreateAsync(new IdentityRole("customer"));
            }

            await userManager.AddToRoleAsync(user, dto.Role);

            return mapper.Map<UserDto>(user);
        }
    }
}