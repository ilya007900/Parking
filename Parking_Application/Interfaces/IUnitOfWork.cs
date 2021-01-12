using Parking_Domain.ParkingEntities;
using System.Linq;

namespace Parking_Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IQueryable<Parking> Parkings { get; }
    }
}
