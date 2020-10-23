using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcDogDaycare.Models;
using ui.MvcDogDaycare.Data;

namespace MvcDogDaycare.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly DogDaycareContext _context;

        public ReservationsController(DogDaycareContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservation.Select(reservation => new Reservation
            {
                Id = reservation.Id,
                DropOffDttm = reservation.DropOffDttm,
                PickUpDttm = reservation.PickUpDttm,
                Pet = reservation.Pet,
                PetId = reservation.PetId
            }).OrderBy(r => r.DropOffDttm).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(record => record.Id == id);

            var dogs = await _context.Dog.ToListAsync();
            ViewBag.ListOfDogs = dogs;
            if (reservation == null)
            {
                return NotFound();
            }
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
                    var dog = await _context.Dog.FirstOrDefaultAsync(dog => dog.Id == reservation.PetId);
                    reservation.Pet = dog;
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
                .Select(res => new Reservation
                {
                    Id = res.Id,
                    DropOffDttm = res.DropOffDttm,
                    PickUpDttm = res.PickUpDttm,
                    Pet = res.Pet,
                    PetId = res.PetId
                })
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