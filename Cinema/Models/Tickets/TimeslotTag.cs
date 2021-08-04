using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.Models.Tickets
{
    public class TimeslotTag
    {
        public int TimeslotId { get; set; }
        public DateTime StartTime { get; set; }
        public decimal Cost { get; set; }

        public string FormattedStartTime => StartTime.ToShortTimeString();

        public string BuyTicketUrl  =>  $"/Tickets/HallInfo?timeslotId={TimeslotId}";
     
    }
}