using Mapster;
using Ticket.Models;
using Ticket.Models.Dtos.Auth;

namespace Ticket.Mapping
{
    public class UserProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserResponseDto>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.Role, src => src.Role);
            config.NewConfig<UserResponseDto, User>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.Role, src => src.Role);
            config.NewConfig<User, CreateUserDto>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.Role, src => src.Role);
            config.NewConfig<CreateUserDto, User>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.Role, src => src.Role);
            config.NewConfig<User, UserLoginDto>()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password);
            config.NewConfig<UserLoginDto, User>()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password);
            config.NewConfig<User, UserLoginResponseDto>()
                .Map(dest => dest.User.Id, src => src.Id)
                .Map(dest => dest.User.Name, src => src.Name)
                .Map(dest => dest.User.Email, src => src.Email)
                .Map(dest => dest.User.Role, src => src.Role);
            config.NewConfig<UserLoginResponseDto, User>()
                .Map(dest => dest.Id, src => src.User.Id)
                .Map(dest => dest.Name, src => src.User.Name)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.Role, src => src.User.Role);
        }
    }
}
