using System.Linq;
using Microsoft.EntityFrameworkCore;
using ParkingService.Domain.Entities;
using ParkingService.Domain.Repositories;

namespace ParkingService.Persistence
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
                .Include(x => x.Floors)
                .ThenInclude(x => x.ParkingSpaces)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
