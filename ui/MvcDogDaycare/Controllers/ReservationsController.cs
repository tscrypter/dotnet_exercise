using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcDogDaycare.Models;
using Steeltoe.Discovery;
using ui.MvcDogDaycare.Data;

namespace MvcDogDaycare.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly DogDaycareContext _context;
        private readonly IDiscoveryClient _discoveryClient;

        public ReservationsController(DogDaycareContext context, IDiscoveryClient discoveryClient)
        {
            _context = context;
            _discoveryClient = discoveryClient;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservation.Select(reservation => new Reservation
            {
                Id = reservation.Id,
                DropOffDttm = reservation.DropOffDttm,
                PickUpDttm = reservation.PickUpDttm,
                Pet = reservation.Pet
            }).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            return View(await _context.Reservation.FirstOrDefaultAsync(record => record.Id == id));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return View(await _context.Reservation.FirstOrDefaultAsync(record => record.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, PetName, DropOffDttm, PickUpDttm")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }

            return View(reservation);
        }

        private bool ReservationExists(int reservationId)
        {
            return _context.Reservation.Any(reservation => reservation.Id == reservationId);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(record => record.Id == id);
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

            var reservation = await _context.Reservation.FindAsync(id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}