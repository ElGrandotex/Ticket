using Ticket.Models;

namespace Ticket.Repository.IRepository
{
    public interface IEventRepository
    {
        ICollection<Event> GetEvents();
        Event? GetEvent(int eventId);
        bool EventExists(int eventId);
        bool EventExists(string eventName);
        Event? GetEventByName(string eventName);
        bool BuyTickets(int eventId, int numberOfTickets);
        bool CreateEvent(Event eventModel);
        bool UpdateEvent(Event eventModel);
        bool DeleteEvent(Event eventModel);
        bool Save();
    }
}
