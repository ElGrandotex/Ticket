using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticket.Data;
using Ticket.Models;
using Ticket.Models.Dtos.Auth;
using Ticket.Repository.IRepository;

namespace Ticket.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _db;
        private string? secretKey;
        public UserRepository(ApplicationDBContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        }
        public User? GetUser(int userId)
        {
            return _db.Users.FirstOrDefault(u => u.Id == userId);
        }

        public ICollection<User> GetUsers()
        {
            return _db.Users.OrderBy(u => u.Name).ToList();
        }

        public bool IsUniqueUser(string email)
        {
            return !_db.Users.Any(u => u.Email.ToLower().Trim() == email.ToLower().Trim());
        }

        public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
        {
            if (string.IsNullOrEmpty(userLoginDto.Email))
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    User = null,
                    Message = "El email es requerido"
                };
            }
            var user = _db.Users.FirstOrDefault<User>(u => u.Email.ToLower().Trim() == userLoginDto.Email.ToLower().Trim());
            if (user == null)
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    User = null,
                    Message = "Usuario no encontrado"
                };
            }
            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
            {
                return new UserLoginResponseDto()
                {
                    Token = "",
                    User = null,
                    Message = "Credenciales incorrectas"
                };
            }
            var handlerToken = new JwtSecurityTokenHandler();
            if (string.IsNullOrWhiteSpace(secretKey))
            { 
                throw new InvalidOperationException("Secret key is not configured.");
            }
            var key = Encoding.UTF8.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("email", user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = handlerToken.CreateToken(tokenDescriptor);
            return new UserLoginResponseDto()
            {
                Token = handlerToken.WriteToken(token),
                User = new UserRegisterDto()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role
                },
                Message = "Login exitoso"
            };
        }

        public async Task<User> Register(CreateUserDto userRegister)
        {
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
            var user = new User
            {
                Name = userRegister.Name,
                Email = userRegister.Email,
                Password = encryptedPassword,
                Role = userRegister.Role
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
