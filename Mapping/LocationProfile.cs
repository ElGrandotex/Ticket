using Mapster;
using Ticket.Models;
using Ticket.Models.Dtos.Location;

namespace Ticket.Mapping
{
    public class LocationProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Location, LocationDto>()
              .Map(dest => dest.LocationId, src => src.LocationId)
              .Map(dest => dest.Name, src => src.Name)
              .Map(dest => dest.Address, src => src.Address)
              .Map(dest => dest.Capacity, src => src.Capacity)
              .Map(dest => dest.City, src => src.City)
              .Map(dest => dest.State, src => src.State);
            config.NewConfig<LocationDto, Location>()
              .Map(dest => dest.LocationId, src => src.LocationId)
              .Map(dest => dest.Name, src => src.Name)
              .Map(dest => dest.Address, src => src.Address)
              .Map(dest => dest.Capacity, src => src.Capacity)
              .Map(dest => dest.City, src => src.City)
              .Map(dest => dest.State, src => src.State);
            config.NewConfig<Location, CreateLocationDto>()
              .Map(dest => dest.Name, src => src.Name)
              .Map(dest => dest.Address, src => src.Address)
              .Map(dest => dest.Capacity, src => src.Capacity)
              .Map(dest => dest.City, src => src.City)
              .Map(dest => dest.State, src => src.State);
            config.NewConfig<CreateLocationDto, Location>()
              .Map(dest => dest.Name, src => src.Name)
              .Map(dest => dest.Address, src => src.Address)
              .Map(dest => dest.Capacity, src => src.Capacity)
              .Map(dest => dest.City, src => src.City)
              .Map(dest => dest.State, src => src.State);
        }
    }
}
