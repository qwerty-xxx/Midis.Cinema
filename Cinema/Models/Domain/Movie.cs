using Cinema.Models.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cinema.Extensions;

namespace Cinema.Models.Domain
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }
        public Genre[] Genres { get; set; }
        public int MinAge { get; set; }
        public string Director { get; set; }
        public string ImgUrl { get; set; }
        public double Rating { get; set; }
        public int? ReleaseDate { get; set; }
        public ICollection<Timeslot> Timeslots { get; set; }
      
        [NotMapped]
        public string FormattedGenres => string.Join(", ", Genres);

        [NotMapped]
        public string FormattedDuration => Duration.ToDuration(); 

    }
}