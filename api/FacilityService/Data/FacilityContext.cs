using FacilityService.Models;
using Microsoft.EntityFrameworkCore;


namespace FacilityService.Data
{
    public class FacilityContext : DbContext
    {
        public FacilityContext (DbContextOptions<FacilityContext> options)
            : base(options)
        {   
        }

        public DbSet<FacilityDAO> Facility { get; set; }
    }
}