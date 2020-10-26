using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacilityService.Data;
using FacilityService.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FacilityService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacilityController : Controller
    {
        private readonly FacilityContext _context;

        public FacilityController(FacilityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Facility>> Get()
        {
            var facilities = await _context.Facility.Select(facilityDao =>
                new Facility
                {
                    Id = facilityDao.Id,
                    Name = facilityDao.Name,
                    City = facilityDao.City,
                    State = facilityDao.State
                }).ToListAsync();

            return facilities ?? new List<Facility>();
        }
    }
}