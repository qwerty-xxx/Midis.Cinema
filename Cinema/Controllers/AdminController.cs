using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace Cinema.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ITicketsService _ticketsService;

       
        public AdminController(ILogger<AdminController> logger, ITicketsService ticketsService)
        {
            _logger = logger;
            _ticketsService = ticketsService;
        }

        public ActionResult FindMovieById(int id)
        {
            var movie = _ticketsService.GetMovieById(id);
            if (movie == null) return Content("Movie with such ID does not exist", "application/json");

            var movieJson = JsonConvert.SerializeObject(movie);
            return Content(movieJson, "application/json");
        }

        public ActionResult FindTimeslotById(int id)
        {
            var timeslot = _ticketsService.GetTimeslotById(id);
            if (timeslot == null) return Content("TimeSlot with such ID does not exist", "application/json");

            var timeslotJson = JsonConvert.SerializeObject(timeslot);
            return Content(timeslotJson, "application/json");
        }

        public ActionResult FindHallById(int id)
        {
            var hall = _ticketsService.GetHallById(id);
            if (hall == null) return Content("Hall with such ID does not exist", "application/json");

            var hallJson = JsonConvert.SerializeObject(hall);
            return Content(hallJson, "application/json");
        }

        public ActionResult MoviesList()
        {
            var movies = _ticketsService.GetAllMovies();
            return View("MoviesList", movies);
        }

        public ActionResult TimeslotsList(int? movieId = null)
        {
            ViewData["MovieId"] = movieId;

            var timeslots = movieId == null
                ? _ticketsService.GetAllTimeslots()
                : _ticketsService.GetTimeslotsByMovieId(movieId.Value);

            var timeSlotGridRow = timeslots.Select(timeslot => new TimeSlotGridRow
            {
                StartTime = timeslot.StartTime,
                Cost = timeslot.Cost,
                Format = timeslot.Format,
                Id = timeslot.Id,
                Hall = timeslot.Hall,
                Movie = timeslot.Movie
            }).ToArray();

            return View("TimeslotsList", timeSlotGridRow);
        }

        public ActionResult HallsList()
        {
            var halls = _ticketsService.GetAllHalls();
            return View("HallsList", halls);
        }

        [HttpGet]
        public ActionResult EditMovie(int movieId)
        {
            var movie = _ticketsService.GetMovieById(movieId);
            return View("EditMovie", movie);
        }

        [HttpPost]
        public ActionResult EditMovie(Movie model)
        {
            if (ModelState.IsValid)
            {
                _ticketsService.UpdateMovie(model);
                return RedirectToAction("MoviesList");
            }

            return View("EditMovie", model);
        }

        [HttpGet]
        public ActionResult EditTimeslot(int timeslotId)
        {
            PopulateHallListAndMoviesList();
            var timeslot = _ticketsService.GetTimeslotById(timeslotId);
            return View("EditTimeslot", timeslot);
        }

        [HttpPost]
        public ActionResult EditTimeslot(Timeslot model)
        {
            if (ModelState.IsValid)
            {
                _ticketsService.UpdateTimeslot(model);
                return RedirectToAction("TimeslotsList", new { movieId = model.MovieId });
            }

            PopulateHallListAndMoviesList();

            return View("EditTimeslot", model);
        }

        [HttpGet]
        public ActionResult EditHall(int hallId)
        {
            var hall = _ticketsService.GetHallById(hallId);

            if (hall == null) return Content("Hall with such ID does not exist", "application/json");

            return View("EditHall", hall);
        }

        [HttpPost]
        public ActionResult EditHall(Hall model)
        {
            if (ModelState.IsValid)
            {
                _ticketsService.UpdateHall(model);
                return RedirectToAction("HallsList");
            }

            return View("EditHall", model);
        }

        [HttpGet]
        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(Movie newMovie)
        {
            if (ModelState.IsValid)
            {
                _ticketsService.CreateMovie(newMovie);
                return RedirectToAction("MoviesList");
            }

            return View(newMovie);
        }

        [HttpGet]
        public ActionResult AddTimeSlot()
        {
            PopulateHallListAndMoviesList();
            return View();
        }

        [HttpPost]
        public ActionResult AddTimeSlot(Timeslot newTimeslot)
        {
            if (ModelState.IsValid)
            {
                _ticketsService.CreateTimeSlot(newTimeslot);
                return RedirectToAction("TimeslotsList");
            }
            PopulateHallListAndMoviesList();

            return View(newTimeslot);
        }

        [HttpGet]
        public ActionResult AddHall()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddHall(Hall newHall)
        {
            if (ModelState.IsValid)
            {
                _ticketsService.CreateHall(newHall);
                return RedirectToAction("HallsList");
            }
            return View(newHall);
        }

        public ActionResult RemoveMovie(int movieId)
        {
            _ticketsService.RemoveMovie(movieId);
            return RedirectToAction("MoviesList");
        }

        public ActionResult RemoveTimeslot(int timeslotId, int? movieId = null)
        {
            _ticketsService.RemoveTimeslot(timeslotId);
            return RedirectToAction("TimeslotsList", new { movieId });
        }

        public ActionResult RemoveHall(int hallId)
        {
            _ticketsService.RemoveHall(hallId);
            return RedirectToAction("HallsList");
        }

        private void PopulateHallListAndMoviesList()
        {
            ViewData["HallsList"] = _ticketsService.GetAllHalls();
            ViewData["MoviesList"] = _ticketsService.GetAllMovies();
        }
    }
}