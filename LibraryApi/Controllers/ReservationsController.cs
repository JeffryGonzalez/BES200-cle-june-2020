using LibraryApi.Domain;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class ReservationsController : ControllerBase
    {
        LibraryDataContext Context;
        ISendReservationsToTheQueue Queue;

        public ReservationsController(LibraryDataContext context, ISendReservationsToTheQueue queue)
        {
            Context = context;
            Queue = queue;
        }

        [HttpPost("/reservations")]
        public async Task<ActionResult<Reservation>> AddReservation([FromBody] Reservation reservationToAdd)
        {
            var numberOfBooksInReservation = reservationToAdd.Books.Count(c => c == ',');
            //for(var t =0; t< numberOfBooksInReservation; t++)
            //{
            //    await Task.Delay(1000); // take a second to process each book.
            //}
            reservationToAdd.ReservationCreated = DateTime.Now;
            reservationToAdd.Status = ReservationStatus.Processing;
            Context.Reservations.Add(reservationToAdd);
            await Context.SaveChangesAsync();
            Queue.SendReservationForProcessing(reservationToAdd);
            return Ok(reservationToAdd);
        }
    }
}
