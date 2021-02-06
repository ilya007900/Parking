using ParkingService.Domain.FunctionalExtensions;

namespace ParkingService.Domain
{
    public class Vehicle
    {
        public LicensePlate LicensePlate { get; set; }

        public int Weight { get; set; }

        private Vehicle(LicensePlate licensePlate, int weight)
        {
            LicensePlate = licensePlate;
            Weight = weight;
        }

        protected Vehicle()
        {
            
        }

        public static Result<Vehicle> Create(LicensePlate licensePlate, int weight)
        {
            if (weight < 0)
            {
                return Result<Vehicle>.Failure("Weight should not be negative");
            }

            return Result<Vehicle>.Success(new Vehicle(licensePlate, weight));
        }
    }
}