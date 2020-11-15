using System;
using System.Collections.Generic;
using System.Linq;
using Parking_Domain.Common;
using Parking_Domain.FunctionalExtensions;
using Parking_Domain.ParkingLevels;
using Parking_Domain.ParkingSpaces;

namespace Parking_Domain.ParkingEntities
{
    public class Parking : Entity
    {
        private readonly List<ParkingLevel> _parkingLevels = new List<ParkingLevel>();

        public virtual IReadOnlyList<ParkingLevel> ParkingLevels => _parkingLevels;

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
            if (_parkingLevels.Any(x => x.Floor == parkingLevel.Floor))
            {
                return Result.Failure($"Parking level with floor {parkingLevel.Floor} already exists");
            }

            _parkingLevels.Add(parkingLevel);
            return Result.Success();
        }

        public void RemoveParkingLevel(ParkingLevel parkingLevel)
        {
            _parkingLevels.Remove(parkingLevel);
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
            foreach (var parkingLevel in _parkingLevels)
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