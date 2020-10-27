using System.Collections.Generic;
using System.Threading.Tasks;
using MvcDogDaycare.Models;

namespace MvcDogDaycare.Services
{
    public interface IDog
    {
        Task<List<DogDao>> GetDogs();
        Task<DogDao> GetDog(int petId);
    }
}