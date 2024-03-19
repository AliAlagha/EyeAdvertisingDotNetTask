using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EyeAdvertisingDotNetTask.Data;
using EyeAdvertisingDotNetTask.Data.DbEntities;
using EyeAdvertisingDotNetTask.Core.ViewModels.Auth;
using EyeAdvertisingDotNetTask.Core.Dtos.Auth;
using EyeAdvertisingDotNetTask.Core.Exceptions;
using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.Options;
using Microsoft.Extensions.Options;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly JwtConfigOptions _jwtConfigOptions;

        public AuthService(ApplicationDbContext context,
                           UserManager<User> userManager,
                           IMapper mapper,
                           SignInManager<User> signInManager,
                           IOptions<JwtConfigOptions> options)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtConfigOptions = options.Value;
        }

        public async Task<string> Register(RegisterDto dto)
        {
            var isEmailExists = await _context.Users
                .AnyAsync(x => x.Email.ToLower().Equals(dto.Email.ToLower()));
            if (isEmailExists)
            {
                throw new UserAlreadyExistsException();
            }

            var user = new User
            {
                Email = dto.Email,
                UserName = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IsActive = true,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true
            };

            string userId = "";
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                userId = user.Id;
            }

            return userId;
        }

        public async Task<LoginResponseViewModel> Login(LoginDto dto)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.UserName.Equals(dto.Username));
            if (user == null)
            {
                throw new InvalidLoginCredentialsException();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
            {
                throw new InvalidLoginCredentialsException();
            }

            return new LoginResponseViewModel
            {
                Token = await GenerateAccessToken(user),
                User = user.FullName
            };
        }

        private async Task<AccessTokenViewModel> GenerateAccessToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>(){
              new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
              new Claim(Claims.UserId, user.Id),
              new Claim(JwtRegisteredClaimNames.Email, user.Email),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            if (roles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, string.Join(",", roles)));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigOptions.SigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddMonths(1);

            var accessToken = new JwtSecurityToken(_jwtConfigOptions.Issuer,
                _jwtConfigOptions.Site,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new AccessTokenViewModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                ExpiredAt = expires
            };
        }

    }
}