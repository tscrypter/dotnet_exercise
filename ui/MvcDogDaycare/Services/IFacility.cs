using System.Collections.Generic;
using System.Threading.Tasks;

namespace MvcDogDaycare.Services
{
    public interface IFacility
    {
        Task<List<Facility>> GetFacilitiesAsync();
        class Facility
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public string State { get; set; }
        }
    }
}