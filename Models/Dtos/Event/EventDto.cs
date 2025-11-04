using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket.Models.Dtos.Event
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; } = null;

        // Foreign Key
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        // Foreign Key
        public int LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
    }
}
