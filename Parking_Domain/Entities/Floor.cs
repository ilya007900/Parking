using System;
using System.Collections.Generic;
using System.Linq;
using ParkingService.Domain.Common;
using ParkingService.Domain.FunctionalExtensions;

namespace ParkingService.Domain.Entities
{
    public class Floor : Entity
    {
        private readonly List<ParkingSpace> parkingSpaces = new List<ParkingSpace>();

        public virtual IReadOnlyList<ParkingSpace> ParkingSpaces => parkingSpaces;

        public int Number { get; private set; }

        public FloorState State { get; private set; }

        public Floor(int number, FloorState state = null) : this()
        {
            Number = number;
            State = state ?? FloorState.Closed;
        }

        protected Floor()
        {

        }

        public void UpdateFloor(int floor)
        {
            Number = floor;
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

        public Result RemoveParkingSpace(int number)
        {
            var parkingSpace = parkingSpaces.FirstOrDefault(x => x.Number == number);
            if (parkingSpace == null)
            {
                return Result.Success();
            }

            if (parkingSpace.State == ParkingSpaceState.Occupied)
            {
                return Result.Failure($"Can't remove parking space: {number}. It is occupied");
            }

            parkingSpaces.Remove(parkingSpace);

            return Result.Success();
        }

        public void UpdateState(FloorState state)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
        }

        public Result FreeParkingSpace(int parkingSpaceNumber)
        {
            var parkingSpace = parkingSpaces.FirstOrDefault(x => x.Number == parkingSpaceNumber);
            if (parkingSpace == null)
            {
                return Result.Failure($"Parking space with number {parkingSpaceNumber} doesn't exist");
            }

            parkingSpace.Free();
            return Result.Success();
        }

        public Result<ParkingSpace> GetParkingSpace(int parkingSpaceNumber)
        {
            var parkingSpace = ParkingSpaces.First(x => x.Number == parkingSpaceNumber);
            if (parkingSpace == null)
            {
                return Result<ParkingSpace>.Failure($"Parking space {parkingSpaceNumber} doesn't exist");
            }

            return Result<ParkingSpace>.Success(parkingSpace);
        }
    }
}