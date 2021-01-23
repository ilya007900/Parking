using System.Collections.Generic;
using System.Linq;
using Parking_Domain.Common;
using Parking_Domain.FunctionalExtensions;

namespace Parking_Domain.Entities
{
    public class ParkingLevel : Entity
    {
        private readonly List<ParkingSpace> parkingSpaces = new List<ParkingSpace>();

        public virtual IReadOnlyList<ParkingSpace> ParkingSpaces => parkingSpaces;

        public int Floor { get; private set; }

        public ParkingLevel(int floor)
        {
            Floor = floor;
        }

        protected ParkingLevel()
        {

        }

        public Result UpdateFloor(int floor)
        {
            //if (Parking.ParkingLevels.Any(x => x != this && x.Floor == floor))
            //{
            //    return Result.Failure($"Level with {floor} floor already exists");
            //}

            Floor = floor;
            return Result.Success();
        }

        public Result AddParkingSpace(ParkingSpace parkingSpace)
        {
            if (parkingSpaces.Any(x => x.Number == parkingSpace.Number))
            {
                return Result.Failure($"Parking space with number {parkingSpace.Number} already exists");
            }

            parkingSpaces.Add(parkingSpace);
            return Result.Success();
        }

        public void RemoveParkingSpace(int number)
        {
            var parkingSpace = parkingSpaces.FirstOrDefault(x => x.Number == number);
            if (parkingSpace != null)
            {
                parkingSpaces.Remove(parkingSpace);
            }
        }

        public Result ParkVehicle(int parkingSpaceNumber, Vehicle vehicle)
        {
            var parkingSpace = parkingSpaces.FirstOrDefault(x => x.Number == parkingSpaceNumber);
            if (parkingSpace == null)
            {
                return Result.Failure($"Parking space with number {parkingSpaceNumber} doesn't exist");
            }

            return parkingSpace.ParkVehicle(vehicle);
        }

        public Result FreeParkingSpace(int parkingSpaceNumber)
        {
            var parkingSpace = parkingSpaces.FirstOrDefault(x => x.Number == parkingSpaceNumber);
            if (parkingSpace == null)
            {
                return Result.Failure($"Parking space with number {parkingSpaceNumber} doesn't exist");
            }

            parkingSpace.FreeParkingSpace();
            return Result.Success();
        }

        public ParkingSpace FindParkingSpace(LicensePlate licensePlate)
        {
            return parkingSpaces.Where(x => x.Vehicle != null)
                .FirstOrDefault(x => x.Vehicle.LicensePlate == licensePlate);
        }
    }
}