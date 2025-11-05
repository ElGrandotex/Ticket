using Ticket.Models;
using Ticket.Models.Dtos.Auth;

namespace Ticket.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User? GetUser(int userId);
        bool IsUniqueUser(string email);
        Task<UserLoginResponseDto> Login(UserLoginDto userLogin);
        Task<User> Register(CreateUserDto userRegister);
    }
}
