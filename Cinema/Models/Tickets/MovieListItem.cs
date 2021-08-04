using Cinema.Models.Domain;
using System.Collections.Generic;

namespace Cinema.Models.Tickets
{
    public class MovieListItem
    {
        public Movie Movie { get; set; }
        public ICollection<TimeslotTag> AvailableTimeslots { get; set; }
    }
}