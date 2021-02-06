using System;
using System.Linq;
using ParkingService.Domain.Dtos;
using ParkingService.Domain.Entities;
using ParkingService.Domain.Extensions;
using ParkingService.Domain.FunctionalExtensions;

namespace ParkingService.Domain.Services
{
    public class ParkingManager
    {
        public Result OpenParking(Parking parking)
        {
            if (parking.State == ParkingState.Open)
            {
                return Result.Success();
            }

            if (parking.Floors.All(x => x.State == FloorState.Closed))
            {
                return Result.Failure("Can't open parking without one open floor");
            }

            parking.UpdateState(ParkingState.Open);

            parking.AddEvent(ParkingEvent.Create("Parking opened."));

            return Result.Success();
        }

        public Result CloseParking(Parking parking, string reason)
        {
            if (parking.State == ParkingState.Closed)
            {
                return Result.Success();
            }

            if (string.IsNullOrWhiteSpace(reason))
            {
                return Result.Failure("Can't close parking without reason.");
            }

            foreach (var floor in parking.Floors)
            {
                if (!floor.CanClose())
                {
                    return Result.Failure($"Can't close floor: {floor.Number}. Floor has occupied parking spaces");
                }
            }

            parking.UpdateState(ParkingState.Closed);

            parking.AddEvent(ParkingEvent.Create($"Parking closed. Reason: {reason}"));

            return Result.Success();
        }

        public void OpenFloor(Parking parking, int floorNumber)
        {
            var floor = parking.Floors.FirstOrDefault(x => x.Number == floorNumber);
            if (floor == null)
            {
                return;
            }

            if (floor.State == FloorState.Open)
            {
                return;
            }

            floor.UpdateState(FloorState.Open);
        }

        public void CloseFloor(Parking parking, int floorNumber)
        {
            var floor = parking.Floors.FirstOrDefault(x => x.Number == floorNumber);
            if (floor == null)
            {
                return;
            }

            if (floor.State == FloorState.Closed)
            {
                return;
            }

            if (!floor.CanClose())
            {
                throw new Exception($"Can't close floor: {floor.Number}. Floor has occupied parking spaces");
            }

            floor.UpdateState(FloorState.Closed);
        }

        public Result ParkVehicle(Parking parking, int floorNumber, int parkingSpaceNumber, Vehicle vehicle)
        {
            var floor = parking.GetFloor(floorNumber);
            if (!floor.IsSuccess)
            {
                return Result.Failure(floor.ErrorMessage);
            }

            var parkingSpace = floor.Value.GetParkingSpace(parkingSpaceNumber);
            if (!parkingSpace.IsSuccess)
            {
                return Result.Failure(parkingSpace.ErrorMessage);
            }

            parkingSpace.Value.ParkVehicle(vehicle);
            return Result.Success();
        }

        public Result FreeParkingSpace(Parking parking, int floorNumber, int parkingSpaceNumber)
        {
            var floor = parking.GetFloor(floorNumber);
            if (!floor.IsSuccess)
            {
                return Result.Failure(floor.ErrorMessage);
            }

            var parkingSpace = floor.Value.GetParkingSpace(parkingSpaceNumber);
            if (!parkingSpace.IsSuccess)
            {
                return Result.Failure(parkingSpace.ErrorMessage);
            }

            parkingSpace.Value.Free();

            return Result.Success();
        }

        public FindVehicleResultDto FindVehicle(Parking parking, LicensePlate licensePlate)
        {
            foreach (var floor in parking.Floors)
            {
                var parkingSpace = floor.ParkingSpaces
                    .Where(x => x.State == ParkingSpaceState.Occupied)
                    .FirstOrDefault(x => x.Vehicle.LicensePlate == licensePlate);

                if (parkingSpace != null)
                {
                    return new FindVehicleResultDto(parking.Id, parking.Address.ToString(), floor.Number,
                        parkingSpace.Number);
                }
            }

            return null;
        }
    }
}