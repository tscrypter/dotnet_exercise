using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcDogDaycare.Models;
using MvcDogDaycare.Services;

namespace MvcDogDaycare.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IFacility _facilityService;
        private readonly IDog _dogService;
        private readonly IReservation _reservationService;

        public ReservationsController(
            IFacility facilityService, 
            IDog dogService, 
            IReservation reservationService)
        {
            _facilityService = facilityService;
            _dogService = dogService;
            _reservationService = reservationService;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var facilities = await _facilityService.GetFacilitiesAsync();
            var dogs = await _dogService.GetDogs();
            var reservations = await _reservationService.GetReservations();
            var reservationsResults = reservations.Select(r => new Reservation
            {
                DropOffDttm = r.DropOffDttm,
                FacilityId = r.FacilityId,
                Id = r.Id,
                PetId = r.PetId,
                Pet = dogs.First(d => d.Id == r.PetId),
                PickUpDttm = r.PickUpDttm
            }).ToList();

            return View(reservationsResults);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReservation(
            [Bind("PetId, FacilityId, DropOffDttm, PickUpDttm")] Reservation reservation)
        {
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDog = _dogService.GetDog(reservation.PetId);
            if (reservationDog == null)
            {
                return NotFound();
            }

            reservation.Pet = await reservationDog;

            await _reservationService.CreateReservation(reservation);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            var facilities = await _facilityService.GetFacilitiesAsync();
            ViewBag.Facilities = facilities;
            
            var dogs = await _dogService.GetDogs();
            ViewBag.ListOfDogs = dogs;
            
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservation(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }
            
            return View(reservation);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservation(id.Value);

            if (reservation == null)
            {
                return NotFound();
            }
            
            var dogs = await _dogService.GetDogs();
            ViewBag.ListOfDogs = dogs;

            var facilities = await _facilityService.GetFacilitiesAsync();
            ViewBag.Facilities = facilities;
            
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, PetId, DropOffDttm, PickUpDttm")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dog = await _dogService.GetDog(reservation.PetId);
                    reservation.Pet = dog;
                    await _reservationService.UpdateReservation(reservation);
                }
                catch (DbUpdateException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction("Index");
            }

            return View(reservation);
        }

        private bool ReservationExists(int reservationId)
        {
            return _reservationService.DoesReservationExist(reservationId);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservation(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservation(id.Value);
            await _reservationService.DeleteReservation(reservation);
            return RedirectToAction("Index");
        }
    }
}