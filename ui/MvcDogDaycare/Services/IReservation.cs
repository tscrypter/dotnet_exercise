using System.Collections.Generic;
using System.Threading.Tasks;
using MvcDogDaycare.Models;

namespace MvcDogDaycare.Services
{
    public interface IReservation
    {
        Task<List<Reservation>> GetReservations();
        Task<Reservation> GetReservation(int reservationId);
        bool DoesReservationExist(int reservationId);
        Task<int> CreateReservation(Reservation reservation);
        Task<int> UpdateReservation(Reservation reservation);

        Task<int> DeleteReservation(Reservation reservation);
    }
}