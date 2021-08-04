using Cinema.Data;
using Cinema.Interfaces;
using Cinema.Models.Domain;
using Cinema.Models.Tickets;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Cinema.Services
{
    public class EntityTicketsService : ITicketsService
    {
        private readonly CinemaContext _context;

        public EntityTicketsService(CinemaContext context)
        {
            _context = context;
        }

        public Movie GetMovieById(int id)
        {
            return _context.Movies.First(x => x.Id == id);
        }

        public Movie[] GetAllMovies()
        {
            return _context.Movies.ToArray();
        }

        private MovieListItem[] GetMoviesInfo(string term = "")
        {
            IQueryable<Movie> movieQuery = _context.Movies;

            if (!string.IsNullOrEmpty(term))
            {
                movieQuery = _context.Movies.Where(x => x.Title.Contains(term));
            }

            return movieQuery
                .ToArray()
                .Select(movie => new MovieListItem
                {
                    Movie = movie,
                    AvailableTimeslots = _context.TimeSlots
                        .Where(timeslot => timeslot.MovieId == movie.Id)
                        .Select(timeslot => new TimeslotTag { Cost = timeslot.Cost, StartTime = timeslot.StartTime, TimeslotId = timeslot.Id})
                        .ToArray()
                }).ToArray();
        }

        public MovieListItem[] GetFullMoviesInfo()
        {
            return GetMoviesInfo();
        }

        public void RemoveMovie(int id)
        {
            var movie = _context.Movies.First(x => x.Id == id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }

        public Hall GetHallById(int id)
        {
            return _context.Halls.First(x => x.Id == id);
        }

        public Hall[] GetAllHalls()
        {
            return _context.Halls.ToArray();
        }

        public void UpdateHall(Hall updatedHall)
        {
            _context.Halls.Update(updatedHall);
            _context.SaveChanges();
        }

        public void RemoveHall(int id)
        {
            var hall = _context.Halls.First(x => x.Id == id);
            _context.Halls.Remove(hall);
            _context.SaveChanges();
        }

        public Timeslot GetTimeslotById(int id)
        {
            return _context.TimeSlots.Include(e=>e.Hall).Include(x => x.RequestedSeats).First(x => x.Id == id);
        }

        public Timeslot[] GetAllTimeslots()
        {
            return _context.TimeSlots
                .Include(x => x.Movie).Where(x => x.Movie != null)
                .Include(x => x.Hall).Where(x => x.Hall != null)
                .ToArray();
        }

        public void UpdateTimeslot(Timeslot updatedTimeslot)
        {
            _context.TimeSlots.Update(updatedTimeslot);
            _context.SaveChanges();
        }

        public Timeslot[] GetTimeslotsByMovieId(int movieId)
        {
            return _context.TimeSlots.Where(x => x.MovieId == movieId).ToArray();
        }

        public void RemoveTimeslot(int id)
        {
            var timeslot = _context.TimeSlots.First(x => x.Id == id);
            _context.TimeSlots.Remove(timeslot);
            _context.SaveChanges();
        }

        public void UpdateMovie(Movie updatedMovie)
        {
            _context.Movies.Update(updatedMovie);
            _context.SaveChanges();
        }

        public void CreateMovie(Movie newMovie)
        {
            _context.Movies.Add(newMovie);
            _context.SaveChanges();
        }

        public void CreateTimeSlot(Timeslot newTimeslot)
        {
            _context.TimeSlots.Add(newTimeslot);
            _context.SaveChanges();
        }

        public bool AddRequestedSeatsToTimeSlot(SeatsProcessRequest seatsRequest)
        {
            var timeslot = _context.TimeSlots.Include(x => x.RequestedSeats).First(x => x.Id == seatsRequest.TimeslotId);

            var requestToProcess = new List<TimeslotSeatRequest>();

            foreach (var seat in seatsRequest.SeatsRequest.AddedSeats)
            {
                requestToProcess.Add(new TimeslotSeatRequest
                {
                    Row = seat.Row,
                    Seat = seat.Seat,
                    Status = seatsRequest.SelectedStatus
                });
            }

            timeslot.RequestedSeats = timeslot.RequestedSeats.Concat(requestToProcess).ToList();

            _context.TimeSlots.Update(timeslot);
            _context.SaveChanges();

            return true;
        }

        public void CreateHall(Hall newHall)
        {
            _context.Halls.Add(newHall);
            _context.SaveChanges();
        }

        public MovieListItem[] SearchMoviesByTerm(string term)
        {
            return GetMoviesInfo(term);
        }
    }
}