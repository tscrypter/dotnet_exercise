using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Facility = MvcDogDaycare.Services.IFacility.Facility;

namespace MvcDogDaycare.Services
{
    public class FacilityService : BaseDiscoveryService, IFacility
    {
        private const string FacilitiesUrl = "http://FacilityService/facility";

        public FacilityService(HttpClient client, ILogger<FacilityService> logger) 
            : base(client, logger)
        {
        }

        public async Task<List<Facility>> GetFacilitiesAsync()
        {
            var facilitiesResponse = 
                await Invoke<List<Facility>>(new HttpRequestMessage(HttpMethod.Get, FacilitiesUrl));
            return facilitiesResponse ?? new List<Facility>();
        }

        public Task<Facility> GetFacilityDetailsAsync(int facilityId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddFacilityAsync(Facility facility)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveFacilityAsync(Facility facility)
        {
            throw new System.NotImplementedException();
        }
    }
}