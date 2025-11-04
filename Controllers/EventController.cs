using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Models.Dtos.Category;
using Ticket.Models.Dtos.Event;
using Ticket.Repository;
using Ticket.Repository.IRepository;

namespace Ticket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public EventController(IEventRepository eventRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetEvents()
        {
            var events = _eventRepository.GetEvents();
            var eventsDto = _mapper.Map<List<EventDto>>(events);
            return Ok(eventsDto);
        }

        [HttpGet("{id:int}", Name = "GetEvent")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetEvent(int id)
        {
            var eventA = _eventRepository.GetEvent(id);
            if (eventA == null)
            {
                return NotFound("El evento no existe");
            }
            var eventDto = _mapper.Map<EventDto>(eventA);
            return Ok(eventDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateEvent([FromBody] CreateEventDto createEventDto)
        {
            if (createEventDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_eventRepository.EventExists(createEventDto.Name))
            {
                ModelState.AddModelError("", "El evento ya existe");
                return StatusCode(409, ModelState);
            }
            var eventA = _mapper.Map<Ticket.Models.Event>(createEventDto);
            if (!_eventRepository.CreateEvent(eventA))
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el registro {eventA.Name}");
                return StatusCode(500, ModelState);
            }
            var createdEvent = _eventRepository.GetEvent(eventA.EventId);
            var eventDto = _mapper.Map<EventDto>(createdEvent);
            return CreatedAtRoute("GetEvent", new { id = eventA.EventId }, eventDto);
        }

        [HttpPut("{id:int}", Name = "UpdateEvent")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateEvent(int id, [FromBody] UpdateEventDto updateEventDto)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            if (!_eventRepository.EventExists(id))
            {
                return NotFound("El evento no existe");
            }
            if (updateEventDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.CategoryExists(updateEventDto.CategoryId))
            {
                return NotFound("La categoría asociada no existe");
            }
            var eventA = _mapper.Map<Ticket.Models.Event>(updateEventDto);
            eventA.EventId = id;
            if (!_eventRepository.UpdateEvent(eventA))
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar el registro {eventA.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteEvent")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteEvent(int id)
        {
            if (!_eventRepository.EventExists(id))
            {
                return NotFound("El evento no existe");
            }
            var eventA = _eventRepository.GetEvent(id);
            if (!_eventRepository.DeleteEvent(eventA))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar el registro {eventA.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpGet("searchByCategory/{id:int}", Name = "GetEventByCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetEventByCategory(int id)
        {
            var events = _eventRepository.GetEventByCategory(id);
            if(events.Count == 0)
            {
                return NotFound("La categoría no existe o no tiene eventos asociados");
            }
            var eventsDto = _mapper.Map<List<EventDto>>(events);
            return Ok(eventsDto);
        }

        [HttpGet("searchByNameDescripton/{searchTerm}", Name = "SearchEvents")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SearchEvents(string searchTerm)
        {
            var events = _eventRepository.SearchEvents(searchTerm);
            if (events.Count == 0)
            {
                return NotFound("No se encontraron eventos que coincidan con la búsqueda");
            }
            var eventsDto = _mapper.Map<List<EventDto>>(events);
            return Ok(eventsDto);
        }

        [HttpPatch("buyEvent/{id}/{quantity}", Name = "BuyEvents")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult BuyEvents(int id, int quantity)
        {
            if (!_eventRepository.EventExists(id))
            {
                return NotFound("El evento no existe");
            }
            if(!_eventRepository.BuyTickets(id, quantity))
            {
                return BadRequest("No hay suficientes entradas disponibles para este evento");
            }
            return Ok("Compra realizada con éxito");
        }
    }
}
