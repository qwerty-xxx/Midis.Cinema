using Cinema.Models.Domain;
using Cinema.Models.Domain.Enums;
using System;

namespace Cinema.Models
{
    public class TimeSlotGridRow
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public decimal Cost { get; set; }
        public Movie Movie { get; set; }
        public Hall Hall { get; set; }
        public Format Format { get; set; }
    }
}