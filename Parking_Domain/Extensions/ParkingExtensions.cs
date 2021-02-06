using System.Linq;
using ParkingService.Domain.Entities;

namespace ParkingService.Domain.Extensions
{
    public static class ParkingExtensions
    {
        public static bool CanClose(this Floor floor)
        {
            return floor.ParkingSpaces.Any(x => x.State == ParkingSpaceState.Occupied);
        }
    }
}