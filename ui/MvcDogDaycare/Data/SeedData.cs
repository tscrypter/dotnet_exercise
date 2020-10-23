using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcDogDaycare.Models;

namespace ui.MvcDogDaycare.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DogDaycareContext(
                serviceProvider.GetRequiredService<DbContextOptions<DogDaycareContext>>()))
            {
                // Look for any reservations
                if (context.Reservation.Any())
                {
                    return; // DB has already been seeded
                }

                
                context.Reservation.AddRange(
                    new Reservation
                    {
                        DropOffDttm = DateTime.Today.AddDays(1).AddHours(8),
                        PickUpDttm = DateTime.Today.AddDays(1).AddHours(17),
                        PetName = "Fido"
                    },
                    new Reservation
                    {
                        DropOffDttm = DateTime.Today.AddDays(2).AddHours(8),
                        PickUpDttm = DateTime.Today.AddDays(2).AddHours(17),
                        PetName = "Fido"
                    },
                    new Reservation
                    {
                        DropOffDttm = DateTime.Today.AddDays(3).AddHours(8),
                        PickUpDttm = DateTime.Today.AddDays(3).AddHours(17),
                        PetName = "Fido"
                    },
                    new Reservation
                    {
                        DropOffDttm = DateTime.Today.AddDays(4).AddHours(8),
                        PickUpDttm = DateTime.Today.AddDays(4).AddHours(17),
                        PetName = "Fido"
                    });
                context.SaveChanges();
            }
        }
    }
}