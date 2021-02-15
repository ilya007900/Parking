using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParkingService.Domain.Entities;
using ParkingService.Domain.Repositories;

namespace ParkingService.Persistence
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly ParkingDbContext dbContext;

        public ParkingRepository(ParkingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<Parking> Get()
        {
            return dbContext.Parkings
                .Include(x => x.Floors)
                .ThenInclude(x => x.ParkingSpaces)
                .ToList();
        }

        public Parking Add(Parking parking)
        {
            return dbContext.Parkings.Add(parking).Entity;
        }

        public void Remove(int id)
        {
            var parking = dbContext.Parkings.Find(id);
            if (parking != null)
            {
                dbContext.Parkings.Remove(parking);
            }
        }

        public Parking Find(int id)
        {
            return dbContext.Parkings
                .Include(x => x.Floors)
                .ThenInclude(x => x.ParkingSpaces)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
