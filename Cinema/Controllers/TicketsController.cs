using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Models.Tickets;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using Cinema.Models.Domain;

namespace Cinema.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        public ActionResult Movies()
        {
            var allMovies = _ticketsService.GetFullMoviesInfo();
            return View("~/Views/Tickets/MoviesList.cshtml", allMovies);
        }

        public ActionResult HallInfo(int timeslotId)
        {
            var timeSlot = _ticketsService.GetTimeslotById(timeslotId);

            var model = new HallInfo
            {
                ColumnsCount = timeSlot.Hall.ColumnsCount,
                RowsCount = timeSlot.Hall.RowsCount,
                TicketCost = timeSlot.Cost,
                CurrentTimeSlotId = timeslotId,
                RequestedSeats = timeSlot.RequestedSeats?.ToArray() ?? Enumerable.Empty<TimeslotSeatRequest>().ToArray()
            };

            return View("HallInfo", model);
        }

        [HttpPost]
        public string ProcessRequest([FromBody] SeatsProcessRequest request)
        {
            var result = _ticketsService.AddRequestedSeatsToTimeSlot(request);

            return JsonConvert.SerializeObject(new { requestResult = result });
        }

        public class SearchTerm
        {
            public string Term { get; set; }
        }

        [HttpPost]
        public string SearchFilms([FromBody] SearchTerm term)
        {
            var results = _ticketsService.SearchMoviesByTerm(term.Term);

            return JsonConvert.SerializeObject(new { Result = results });
        }
    }
}