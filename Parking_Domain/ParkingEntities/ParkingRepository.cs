using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Parking_Domain.ParkingEntities
{
    public class ParkingRepository
    {
        private readonly ParkingContext _context;

        public ParkingRepository(ParkingContext context)
        {
            _context = context;
        }

        public Parking FindById(int id)
        {
            var parking = _context.ParkingEntities.Find(id);
            if (parking == null)
            {
                return null;
            }

            _context.Entry(parking).Collection(x=>x.ParkingLevels).Load();
            return parking;
        }

        public IEnumerable<Parking> GetAll()
        {
            return _context.ParkingEntities
                .Include(x => x.ParkingLevels)
                .ThenInclude(x => x.ParkingSpaces);
        }
    }
}