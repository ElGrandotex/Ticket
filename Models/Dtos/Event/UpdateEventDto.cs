using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket.Models.Dtos.Event
{
    public class UpdateEventDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
        public DateTime? LastUpdatedAt { get; set; } = null;

        // Foreign Key
        public int CategoryId { get; set; }

        // Foreign Key
        public int LocationId { get; set; }
    }
}
