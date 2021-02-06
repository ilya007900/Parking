using System;
using System.Collections.Generic;
using System.Linq;
using ParkingService.Domain.Common;
using ParkingService.Domain.FunctionalExtensions;

namespace ParkingService.Domain.Entities
{
    public class Parking : Entity
    {
        private readonly List<Floor> floors = new List<Floor>();
        //private readonly List<ParkingEvent> events = new List<ParkingEvent>();

        public virtual IReadOnlyList<Floor> Floors => floors;
        //public virtual IReadOnlyList<ParkingEvent> Events => events;

        public int ParkingSpaces => Floors.Sum(x => x.ParkingSpaces.Count);

        public virtual Address Address { get; private set; }

        public virtual ParkingState State { get; private set; }

        public Parking(Address address, ParkingState state = null) : this()
        {
            Address = address;
            State = state ?? ParkingState.Closed;
        }

        //For EF
        protected Parking()
        {
            
        }

        public Result AddFloor(Floor floor)
        {
            if (floors.Any(x => x.Number == floor.Number))
            {
                return Result.Failure($"Floor {floor.Number} already exists");
            }

            floors.Add(floor);
            return Result.Success();
        }

        public Result RemoveFloor(int floor)
        {
            var parkingLevel = floors.FirstOrDefault(x => x.Number == floor);
            if (parkingLevel == null)
            {
                return Result.Success();
            }

            if (parkingLevel.ParkingSpaces.Any(x => x.State == ParkingSpaceState.Occupied))
            {
                return Result.Failure($"Can't remove floor {floor}. Floor has occupied parking spaces");
            }

            floors.Remove(parkingLevel);
            return Result.Success();
        }

        public void AddEvent(ParkingEvent parkingEvent)
        {
            //events.Add(parkingEvent);
        }

        public void UpdateAddress(Address address)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public void UpdateState(ParkingState state)
        {
            State = state ?? throw new ArgumentNullException(nameof(state));
        }

        public Result<Floor> GetFloor(int floorNumber)
        {
            var floor = Floors.FirstOrDefault(x => x.Number == floorNumber);
            if (floor == null)
            {
                return Result<Floor>.Failure($"Floor: {floorNumber} doesn't exist");
            }

            return Result<Floor>.Success(floor);
        }
    }
}