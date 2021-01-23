using System.Linq;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IParkingRepository
    {
        IQueryable<Parking> Get();

        Parking Add(Parking parking);

        Parking Find(int id);
    }
}
