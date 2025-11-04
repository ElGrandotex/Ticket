using Ticket.Data;
using Ticket.Models;
using Ticket.Repository.IRepository;

namespace Ticket.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDBContext _db;
        public LocationRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public bool CreateLocation(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }
            _db.Locations.Add(location);
            return Save();
        }

        public bool DeleteLocation(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }
            _db.Locations.Remove(location);
            return Save();
        }

        public Location? GetLocation(int locationId)
        {
            if (locationId <= 0)
            {
                throw new ArgumentException("Location ID must be greater than zero.");
            }
            return _db.Locations.FirstOrDefault(l => l.LocationId == locationId);
        }

        public ICollection<Location> GetLocations()
        {
            if (_db.Locations == null)
            {
                return new List<Location>();
            }
            return _db.Locations.ToList();
        }

        public bool LocationExists(int locationId)
        {
            if (locationId <= 0)
            {
                throw new ArgumentException("Location ID must be greater than zero.");
            }
            return _db.Locations.Any(l => l.LocationId == locationId);
        }

        public bool LocationExists(string locationName)
        {
            if (string.IsNullOrWhiteSpace(locationName))
            {
                throw new ArgumentException("Location name cannot be null or empty.");
            }
            return _db.Locations.Any(l => l.Name.ToLower().Trim() == locationName.ToLower().Trim());
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdateLocation(Location location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }
            _db.Locations.Update(location);
            return Save();
        }
    }
}
