using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcDogDaycare.Models;
using ui.MvcDogDaycare.Data;

namespace MvcDogDaycare.Services
{
    public class ReservationService : IReservation
    {
        private DogDaycareContext _context;

        public ReservationService(DogDaycareContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetReservations()
        {
            return await _context.Reservation.Select(reservation => new Reservation
            {
                Id = reservation.Id,
                DropOffDttm = reservation.DropOffDttm,
                PickUpDttm = reservation.PickUpDttm,
                Pet = reservation.Pet,
                PetId = reservation.PetId
            }).OrderBy(r => r.DropOffDttm).ToListAsync();
        }

        public async Task<Reservation> GetReservation(int reservationId)
        {
            return await _context.Reservation
                .FirstOrDefaultAsync(record => record.Id == reservationId);
        }

        public bool DoesReservationExist(int reservationId)
        {
            return _context.Reservation.Any(reservation => reservation.Id == reservationId);
        }

        public async Task<int> CreateReservation(Reservation reservation)
        {
            await _context.Reservation.AddAsync(reservation);
            return await _context.SaveChangesAsync();
        }

        public Task<int> UpdateReservation(Reservation reservation)
        {
            _context.Update(reservation);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteReservation(Reservation reservation)
        {
            _context.Reservation.Remove(reservation);
            return _context.SaveChangesAsync();
        }
    }
}