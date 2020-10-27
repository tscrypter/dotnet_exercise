using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcDogDaycare.Models;
using ui.MvcDogDaycare.Data;

namespace MvcDogDaycare.Services
{
    public class Dog : IDog
    {
        private DogDaycareContext _context;

        public Dog(DogDaycareContext context)
        {
            _context = context;
        }

        public async Task<List<DogDao>> GetDogs()
        {
            return await _context.Dog.ToListAsync();
        }

        public async Task<DogDao> GetDog(int petId)
        {
            return await _context.Dog
                .Where(dog => dog.Id == petId)
                .FirstOrDefaultAsync();
        }
    }
}