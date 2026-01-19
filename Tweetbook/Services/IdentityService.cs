using Microsoft.AspNetCore.Identity;
using System.Text;
using Tweetbook.Domain;
using Tweetbook.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


namespace Tweetbook.Services
{


    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _UserManager;
        private readonly JwtSettings _jwtsettings;
        public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _UserManager = userManager;
            _jwtsettings = new JwtSettings();
        }
        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await _UserManager.FindByNameAsync(email);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "user with this eamil already exists" }
                };
            }

            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email
            };

            var createdUser = await _UserManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {

                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)

                };

            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Id,email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult
            {
                Token = tokenHandler.WriteToken(token),

            };


        }
    }
}
