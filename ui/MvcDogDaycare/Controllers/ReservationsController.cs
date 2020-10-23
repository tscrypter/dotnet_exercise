using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return View(await _context.Reservation.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _context.Reservation.FirstOrDefaultAsync(record => record.Id == id));
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _context.Reservation.FirstOrDefaultAsync(record => record.Id == id));
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _context.Reservation.FirstOrDefaultAsync(record => record.Id == id));
        }
    }
}