using Ticket.Models;

namespace Ticket.Repository.IRepository
{
    public interface ILocationRepository
    {
        ICollection<Location> GetLocations();
        Location? GetLocation(int locationId);
        bool LocationExists(int locationId);
        bool LocationExists(string locationName);
        bool CreateLocation(Location location);
        bool UpdateLocation(Location location);
        bool DeleteLocation(Location location);
        bool Save();
    }
}
