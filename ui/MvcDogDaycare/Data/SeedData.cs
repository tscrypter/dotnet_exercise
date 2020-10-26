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
                context.Database.Migrate();
                // Look for any reservations
                if (context.Reservation.Any())
                {
                    return; // DB has already been seeded
                }

                var fido = new Dog
                {
                    ContactNumber = "555-555-5555",
                    DateOfBirth = DateTime.Today.AddYears(-6),
                    Name = "Fido"
                };
                var clifford = new Dog
                {
                    ContactNumber = "111-111-1111",
                    DateOfBirth = DateTime.Today.AddYears(-1),
                    Name = "Clifford"
                };
                context.Dog.AddRange(fido, clifford);                
                context.Reservation.AddRange(
                    new Reservation
                    {
                        DropOffDttm = DateTime.Today.AddDays(1).AddHours(8),
                        PickUpDttm = DateTime.Today.AddDays(1).AddHours(17),
                        Pet = fido,
                        FacilityId = 1
                    },
                    new Reservation
                    {
                        DropOffDttm = DateTime.Today.AddDays(2).AddHours(8),
                        PickUpDttm = DateTime.Today.AddDays(2).AddHours(17),
                        Pet = fido,
                        FacilityId = 1
                    },
                    new Reservation
                    {
                        DropOffDttm = DateTime.Today.AddDays(3).AddHours(8),
                        PickUpDttm = DateTime.Today.AddDays(3).AddHours(17),
                        Pet = fido,
                        FacilityId = 1
                    },
                    new Reservation
                    {
                        DropOffDttm = DateTime.Today.AddDays(4).AddHours(8),
                        PickUpDttm = DateTime.Today.AddDays(4).AddHours(17),
                        Pet = fido,
                        FacilityId = 1
                    },
                    new Reservation
                    {
                        DropOffDttm = DateTime.Today.AddDays(1).AddHours(8),
                        PickUpDttm = DateTime.Today.AddDays(1).AddHours(17),
                        Pet = clifford,
                        FacilityId = 1
                    },
                    new Reservation
                    {
                        DropOffDttm = DateTime.Today.AddDays(3).AddHours(8),
                        PickUpDttm = DateTime.Today.AddDays(3).AddHours(17),
                        Pet = clifford,
                        FacilityId = 2
                    });
                
                context.SaveChanges();
            }
        }
    }
}