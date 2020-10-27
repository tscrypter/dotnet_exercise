using Microsoft.EntityFrameworkCore;
using MvcDogDaycare.Models;

namespace ui.MvcDogDaycare.Data
{
    public class DogDaycareContext : DbContext
    {
        public DogDaycareContext (DbContextOptions<DogDaycareContext> options)
            : base(options)
        {   
        }

        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<DogDao> Dog { get; set; }
    }
}