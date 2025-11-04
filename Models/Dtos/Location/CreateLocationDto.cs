namespace Ticket.Models.Dtos.Location
{
    public class CreateLocationDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}
