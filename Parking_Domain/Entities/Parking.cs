using System;
using System.Collections.Generic;
using System.Linq;
using Parking_Domain.Common;
using Parking_Domain.FunctionalExtensions;

namespace Parking_Domain.Entities
{
    public class Parking : Entity
    {
        private readonly List<ParkingLevel> parkingLevels = new List<ParkingLevel>();

        public virtual IReadOnlyList<ParkingLevel> ParkingLevels => parkingLevels;

        public virtual Address Address { get; private set; }

        public Parking(Address address) : this()
        {
            Address = address;
        }

        //For EF
        protected Parking()
        {
            
        }

        public Result AddParkingLevel(ParkingLevel parkingLevel)
        {
            if (parkingLevels.Any(x => x.Floor == parkingLevel.Floor))
            {
                return Result.Failure($"Parking level with floor {parkingLevel.Floor} already exists");
            }

            parkingLevels.Add(parkingLevel);
            return Result.Success();
        }

        public void RemoveParkingLevel(int floor)
        {
            var parkingLevel = parkingLevels.FirstOrDefault(x => x.Floor == floor);
            if (parkingLevel != null)
            {
                parkingLevels.Remove(parkingLevel);
            }
        }

        public void UpdateAddress(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            Address = address;
        }

        public ParkingSpace FindParkingSpace(LicensePlate licensePlate)
        {
            foreach (var parkingLevel in parkingLevels)
            {
                var parkingSpace = parkingLevel.FindParkingSpace(licensePlate);
                if (parkingSpace != null)
                {
                    return parkingSpace;
                }
            }

            return null;
        }
    }
}