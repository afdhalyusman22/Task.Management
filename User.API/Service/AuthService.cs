using Library.Backend.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.API.Entities;
using User.API.Models.Auth;
using User.API.Service.Interfaces;

namespace User.API.Service
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly AppSettingConfig _appConfig;
        public AuthService(DataContext context, IOptions<AppSettingConfig> options)
        {
            _context = context;
            _appConfig = options.Value;
        }

        public async Task<(ValidationError? error, TokenDto? token)> AuthenticateAsync(LoginDto dto, CancellationToken cancellationToken)
        {
            var r = new TokenDto();

            var user = await CheckUser(dto.PhoneEmail, cancellationToken);

            if (user == null)
            {
                return (new ValidationError()
                {
                    Field = nameof(dto.PhoneEmail),
                    Message = "User not found"
                }, null);
            }

            // Validate the password
            var passwordValid = CheckPassword(user, dto.Password);

            if (!passwordValid)
            {
                return (new ValidationError()
                {
                    Field = nameof(dto.PhoneEmail),
                    Message = "Password not valid"
                }, null);
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConfig.Secret);
            int TokenExpired = Convert.ToInt32(_appConfig.TokenExpired);
            string Issuer = _appConfig.Issuer;
            string Audience = _appConfig.Audience;

            var claims = new List<Claim> {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.FullName),
                            new Claim("userId",user.UserId.ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email),
                            };

            TokenExpired = TokenExpired <= 0 ? TokenExpired = 60 : TokenExpired;

            user.LastLogin = DateTime.UtcNow;
            _context.Update(user);
            await _context.SaveChangesAsync();

            var token = new JwtSecurityToken(issuer: Issuer,
                            audience: Audience,
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(TokenExpired),
                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                        );
            r.AccessToken = tokenHandler.WriteToken(token);
            return (null, r);
        }

        public async Task<ValidationError> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken)
        {
            var gender = await _context.Genders
                .Where(Q => Q.GenderId == dto.GenderId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (gender == null)
            {
                return new ValidationError
                {
                    Field = nameof(dto.GenderId),
                    Message = "Gender not valid",
                };
            }

            var userExist = await _context.UserAccounts
                .AnyAsync(Q => Q.Email == dto.Email || Q.PhoneNumber == dto.PhoneNumber, cancellationToken);

            if (userExist)
            {
                return new ValidationError
                {
                    Field = nameof(dto.GenderId),
                    Message = "Email / Phone number already exist",
                };
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new UserAccount
            {
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                FullName = dto.FullName,
                GenderId = dto.GenderId,
                LastLogin = DateTime.UtcNow,
                RegisterAt = DateTime.UtcNow,
                Password = passwordHash,
                UserId = Guid.NewGuid(),
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            return null;
        }

        private async Task<UserAccount> CheckUser(string identifier, CancellationToken cancellationToken)
        {
            // Get use
            var user = await _context.UserAccounts
                .Where(Q => (Q.PhoneNumber == identifier || Q.Email == identifier) && Q.DeletedAt != null)
                .FirstOrDefaultAsync(cancellationToken);

            return user;
        }

        private bool CheckPassword(UserAccount user, string password)
        {
            var userPasswordHash = user.Password;
            var passwordValid = BCrypt.Net.BCrypt.Verify(password, userPasswordHash);

            return passwordValid;
        }


    }
}
