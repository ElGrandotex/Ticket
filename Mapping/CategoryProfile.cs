using Mapster;
using Ticket.Models;
using Ticket.Models.Dtos;

namespace Ticket.Mapping
{
    public class CategoryProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, CategoryDto>()
              .Map(dest => dest.Id, src => src.Id)
              .Map(dest => dest.Name, src => src.Name)
              .Map(dest => dest.CreationDate, src => src.CreationDate);

            config.NewConfig<CategoryDto, Category>()
              .Map(dest => dest.Id, src => src.Id)
              .Map(dest => dest.Name, src => src.Name)
              .Map(dest => dest.CreationDate, src => src.CreationDate);

            config.NewConfig<Category, CreateCategoryDto>()
                  .Map(dest => dest.Name, src => src.Name);

            config.NewConfig<CreateCategoryDto, Category>()
                    .Map(dest => dest.Name, src => src.Name);
        }
    }
}
