using Cinema.Models.Domain.Enums;
using Cinema.Models.Tickets;
using System;
using System.Collections.Generic;

namespace Cinema.Models.Domain
{
    public class Timeslot : BaseEntity
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public DateTime StartTime { get; set; }
        public decimal Cost { get; set; }
        public Format Format { get; set; }
        public ICollection<TimeslotSeatRequest> RequestedSeats { get; set; }
    }
}