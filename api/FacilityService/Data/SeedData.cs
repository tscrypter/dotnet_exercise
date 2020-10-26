using System;
using System.Linq;
using FacilityService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FacilityService.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FacilityContext(
                serviceProvider.GetRequiredService<DbContextOptions<FacilityContext>>()))
            {
                context.Database.Migrate();
                // Look for any reservations
                if (context.Facility.Any())
                {
                    return; // DB has already been seeded
                }
                
                context.Facility.AddRange(
                    new FacilityDAO
                    {
                        Id = 1,
                        City = "Crescent Springs",
                        State = "KY",
                        Name = "Allie's Walkabout",
                        Latitude = "39.037766",
                        Longitude = "-84.592238"
                    },
                    new FacilityDAO
                    {
                        Id = 2,
                        City = "Erlanger",
                        State = "Kentucky",
                        Name="Dog Town",
                        Latitude = "39.0479676",
                        Longitude = "-84.58668"
                    });
                context.SaveChanges();
            }
        }
    }
}