using ParkingService.Domain.Entities;
using System.Collections.Generic;

namespace ParkingService.Domain.Repositories
{
    public interface IParkingRepository
    {
        IList<Parking> Get();

        Parking Add(Parking parking);

        void Remove(int id);

        Parking Find(int id);
    }
}