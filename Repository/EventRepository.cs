using Ticket.Data;
using Ticket.Models;
using Ticket.Repository.IRepository;

namespace Ticket.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDBContext _db;
        public EventRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public bool BuyTickets(int eventId, int numberOfTickets)
        {
            if (numberOfTickets <= 0)
            {
                throw new ArgumentException("Number of tickets must be greater than zero.");
            }
            if (!_db.Events.Any(e => e.EventId == eventId))
            {
                throw new ArgumentException("Event not found.");
            }
            var eventToUpdate = _db.Events.First(e => e.EventId == eventId);
            if (eventToUpdate.Stock < numberOfTickets)
            {
                return false; // Not enough tickets available
            }
            eventToUpdate.Stock -= numberOfTickets;
            _db.Events.Update(eventToUpdate);
            return Save();
        }

        public bool CreateEvent(Event eventModel)
        {
            if(eventModel == null)
            {
                throw new ArgumentNullException(nameof(eventModel));
            }
            eventModel.CreatedAt = DateTime.Now;
            eventModel.LastUpdatedAt = DateTime.Now;
            _db.Events.Add(eventModel);
            return Save();
        }

        public bool DeleteEvent(Event eventModel)
        {
            if(eventModel == null)
            {
                throw new ArgumentNullException(nameof(eventModel));
            }
            _db.Events.Remove(eventModel);
            return Save();
        }

        public bool EventExists(int eventId)
        {
            if(eventId <= 0)
            {
                throw new ArgumentException("Event ID must be greater than zero.");
            }
            return _db.Events.Any(e => e.EventId == eventId);
        }

        public bool EventExists(string eventName)
        {
            if(string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException("Event name cannot be null or empty.");
            }
            return _db.Events.Any(e => e.Name.ToLower().Trim() == eventName.ToLower().Trim());
        }

        public Event? GetEvent(int eventId)
        {
            if(eventId <= 0)
            {
                throw new ArgumentException("Event ID must be greater than zero.");
            }
            return _db.Events.FirstOrDefault(e => e.EventId == eventId);
        }

        public Event? GetEventByName(string eventName)
        {
            if(string.IsNullOrWhiteSpace(eventName))
            {
                throw new ArgumentException("Event name cannot be null or empty.");
            }
            return _db.Events.FirstOrDefault(e => e.Name.ToLower().Trim() == eventName.ToLower().Trim());
        }

        public ICollection<Event> GetEventByCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                return new List<Event>();
            }
            return _db.Events.Where(e => e.CategoryId == categoryId).OrderBy(e => e.Name).ToList();
        }

        public ICollection<Event> GetEvents()
        {
            return _db.Events.OrderBy(e => e.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdateEvent(Event eventModel)
        {
            if(eventModel == null)
            {
                throw new ArgumentNullException(nameof(eventModel));
            }
            eventModel.LastUpdatedAt = DateTime.Now;
            _db.Events.Update(eventModel);
            return Save();
        }

        ICollection<Event> SearchEvent(string eventName)
        {
            IQueryable<Event> query = _db.Events;
            if (!string.IsNullOrWhiteSpace(eventName))
            {
                query = query.Where(e => e.Name.ToLower().Trim().Contains(eventName.ToLower().Trim()));
            }
            return query.OrderBy(e => e.Name).ToList();
        }
    }
}
