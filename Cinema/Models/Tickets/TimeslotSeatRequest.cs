using Cinema.Models.Domain;
using Cinema.Models.Tickets.Enums;

namespace Cinema.Models.Tickets
{
    public class TimeslotSeatRequest : BaseEntity
    {
        public int TimeslotId { get; set; }
        public Timeslot Timeslot { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
        public RequestStatus Status { get; set; }
    }
}