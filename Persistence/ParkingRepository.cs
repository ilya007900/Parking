using System.Linq;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly AppContext dbContext;

        public ParkingRepository(AppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Parking> Get()
        {
            return dbContext.Parkings;
        }

        public Parking Add(Parking parking)
        {
            return dbContext.Parkings.Add(parking).Entity;
        }

        public Parking Find(int id)
        {
            return dbContext.Parkings
                .Include(x => x.ParkingLevels)
                .ThenInclude(x => x.ParkingSpaces)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
