using System.Data.Common;
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
                Pet = reservation.Pet,
                PetId = reservation.PetId
            }).OrderBy(r => r.DropOffDttm).ToListAsync());
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReservation([Bind("PetId, DropOffDttm, PickUpDttm")] Reservation reservation)
        {
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDog = await _context.Dog
                .Where(dog => dog.Id == reservation.PetId)
                .FirstOrDefaultAsync();
            if (reservationDog == null)
            {
                return NotFound();
            }

            reservation.Pet = reservationDog;

            await _context.Reservation.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            var dogs = await _context.Dog.ToListAsync();
            ViewBag.ListOfDogs = dogs;
            
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