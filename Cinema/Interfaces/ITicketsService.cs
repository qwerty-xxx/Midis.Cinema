using Cinema.Models.Domain;
using Cinema.Models.Tickets;

namespace Cinema.Interfaces
{
    public interface ITicketsService
    {
        Movie GetMovieById(int id);
        Movie[] GetAllMovies();
        MovieListItem[] GetFullMoviesInfo();
        void CreateMovie(Movie newMovie);
        void UpdateMovie(Movie updatedMovie);
        void RemoveMovie(int id);

        Hall GetHallById(int id);
        Hall[] GetAllHalls();
        void CreateHall(Hall newHall);
        void UpdateHall(Hall updatedHall);
        void RemoveHall(int id);

        Timeslot GetTimeslotById(int id);
        Timeslot[] GetAllTimeslots();
        Timeslot[] GetTimeslotsByMovieId(int movieId);
        void CreateTimeSlot(Timeslot newTimeslot);
        void UpdateTimeslot(Timeslot updatedTimeslot);
        void RemoveTimeslot(int id);

        bool AddRequestedSeatsToTimeSlot(SeatsProcessRequest request);
        MovieListItem[] SearchMoviesByTerm(string term);
    }
}
