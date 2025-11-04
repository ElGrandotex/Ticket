using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ticket.Models.Dtos.Location;
using Ticket.Repository.IRepository;

namespace Ticket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationController(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetLocations()
        {
            var locations = _locationRepository.GetLocations();
            var locationsDto = _mapper.Map<List<LocationDto>>(locations);
            return Ok(locationsDto);
        }

        [HttpGet("{id:int}", Name = "GetLocation")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetLocation(int id)
        {
            var location = _locationRepository.GetLocation(id);
            if (location == null)
            {
                return NotFound("La ubicación no existe");
            }
            var locationDto = _mapper.Map<LocationDto>(location);
            return Ok(locationDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateLocation([FromBody] CreateLocationDto createLocationDto)
        {
            if (createLocationDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_locationRepository.LocationExists(createLocationDto.Name))
            {
                ModelState.AddModelError("CustomError", "La ubicación ya existe");
                return BadRequest(ModelState);
            }
            var location = _mapper.Map<Models.Location>(createLocationDto);
            if (!_locationRepository.CreateLocation(location))
            {
                ModelState.AddModelError("CustomError", $"Algo salio mal guardando la ubicación {location.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetLocation", new { id = location.LocationId }, location);
        }

        [HttpPatch("{id:int}", Name = "UpdateLocation")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateLocation(int id, [FromBody] CreateLocationDto updateLocationDto)
        {
            if (!_locationRepository.LocationExists(id))
            {
                return NotFound("La ubicación no existe");
            }
            if (updateLocationDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_locationRepository.LocationExists(updateLocationDto.Name))
            {
                ModelState.AddModelError("CustomError", "La ubicación ya existe");
                return BadRequest(ModelState);
            }
            var location = _mapper.Map<Models.Location>(updateLocationDto);
            location.LocationId = id;
            if (!_locationRepository.UpdateLocation(location))
            {
                ModelState.AddModelError("CustomError", $"Algo salio mal al actualizar {location.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteLocation")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLocation(int id)
        {
            if (!_locationRepository.LocationExists(id))
            {
                return NotFound("La ubicación no existe");
            }
            var locationDto = _locationRepository.GetLocation(id);
            if (locationDto == null)
            {
                return NotFound("La ubicación no existe");
            }
            if (!_locationRepository.DeleteLocation(locationDto))
            {
                ModelState.AddModelError("CustomError", $"Algo salio mal al eliminar la ubicación {locationDto.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
