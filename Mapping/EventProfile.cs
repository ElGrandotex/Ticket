using Mapster;
using Ticket.Models;
using Ticket.Models.Dtos.Event;

namespace Ticket.Mapping
{
    public class EventProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Event, EventDto>()
              .Map(dest => dest.EventId, src => src.EventId)
              .Map(dest => dest.Name, src => src.Name)
              .Map(dest => dest.Date, src => src.Date)
              .Map(dest => dest.Description, src => src.Description)
              .Map(dest => dest.Price, src => src.Price)
              .Map(dest => dest.ImgUrl, src => src.ImgUrl)
              .Map(dest => dest.Stock, src => src.Stock)
              .Map(dest => dest.CreatedAt, src => src.CreatedAt)
              .Map(dest => dest.LastUpdatedAt, src => src.LastUpdatedAt)
              .Map(dest => dest.CategoryId, src => src.CategoryId)
              .Map(dest => dest.LocationId, src => src.LocationId)
              .Map(dest => dest.CategoryName, src => src.Category!.Name)
              .Map(dest => dest.LocationName, src => src.Location!.Name);
            config.NewConfig<EventDto, Event>()
                .Map(dest => dest.EventId, src => src.EventId)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Date, src => src.Date)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.ImgUrl, src => src.ImgUrl)
                .Map(dest => dest.Stock, src => src.Stock)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.LastUpdatedAt, src => src.LastUpdatedAt)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.LocationId, src => src.LocationId);
            config.NewConfig<Event, CreateEventDto>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Date, src => src.Date)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.ImgUrl, src => src.ImgUrl)
                .Map(dest => dest.Stock, src => src.Stock)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.LocationId, src => src.LocationId);
            config.NewConfig<CreateEventDto, Event>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Date, src => src.Date)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.ImgUrl, src => src.ImgUrl)
                .Map(dest => dest.Stock, src => src.Stock)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.LocationId, src => src.LocationId);
            config.NewConfig<Event, UpdateEventDto>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Date, src => src.Date)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.ImgUrl, src => src.ImgUrl)
                .Map(dest => dest.Stock, src => src.Stock)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.LocationId, src => src.LocationId);
            config.NewConfig<UpdateEventDto, Event>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Date, src => src.Date)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.ImgUrl, src => src.ImgUrl)
                .Map(dest => dest.Stock, src => src.Stock)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.LocationId, src => src.LocationId);
        }
    }
}
