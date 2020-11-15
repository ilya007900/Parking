using System;
using Parking_Domain.Common;
using Parking_Domain.FunctionalExtensions;
using Parking_Domain.ParkingLevels;

namespace Parking_Domain.ParkingSpaces
{
    public class ParkingSpace : Entity
    {
        public int Number { get; }

        public virtual Vehicle Vehicle { get; private set; }

        public virtual ParkingLevel ParkingLevel { get; }

        public ParkingSpace(int number)
        {
            Number = number;
        }

        public Result ParkVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            if (Vehicle != null)
            {
                return Result.Failure($"Parking space {Number} doesn't empty");
            }

            Vehicle = vehicle;
            return Result.Success();
        }

        public void FreeParkingSpace()
        {
            Vehicle = null;
        }
    }
}