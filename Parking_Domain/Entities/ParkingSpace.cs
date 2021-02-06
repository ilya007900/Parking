using System;
using ParkingService.Domain.Common;
using ParkingService.Domain.FunctionalExtensions;

namespace ParkingService.Domain.Entities
{
    public class ParkingSpace : Entity
    {
        public int Number { get; }

        public ParkingSpaceState State { get; private set; }

        public virtual Vehicle Vehicle { get; private set; }

        public ParkingSpace(int number)
        {
            Number = number;
        }

        public void ParkVehicle(Vehicle vehicle)
        {
            if (State != ParkingSpaceState.Free)
            {
                throw new Exception($"Parking space {Number} doesn't empty");
            }

            Vehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle));
            State = ParkingSpaceState.Occupied;
        }

        public void Free()
        {
            Vehicle = null;
            State = ParkingSpaceState.Free;
        }

        public Result Close()
        {
            if (State == ParkingSpaceState.Occupied)
            {
                return Result.Failure($"Can't close parking space with state {State}");
            }

            State = ParkingSpaceState.Closed;
            return Result.Success();
        }
    }
}