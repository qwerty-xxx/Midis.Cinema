using Cinema.Models.Domain;
using Cinema.Models.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Cinema.Data
{
    public class CinemaContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Timeslot> TimeSlots { get; set; }
        public DbSet<Hall> Halls { get; set; }

        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Movie>()
                .Property(x => x.Genres)
                .HasConversion(
                    v => string.Join(",", v.Select(e => e).ToArray()),
                    v => v.Split(new[] { ',' }).Select(e => Enum.Parse(typeof(Genre), e)).Cast<Genre>().ToArray());
        }
    }
}