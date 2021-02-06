using System.Linq;
using ParkingService.Domain.Entities;

namespace ParkingService.Domain.Repositories
{
    public interface IParkingRepository
    {
        IQueryable<Parking> Get();

        Parking Add(Parking parking);

        Parking Find(int id);
    }
}