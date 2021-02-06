using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.AspNetCore.Builder;
using ParkingService.Domain;
using ParkingService.Domain.Entities;

namespace ParkingService.Persistence
{
    public static class DataProvider
    {
        private const int ParkingsCount = 10;
        private const int FloorsPerParking = 5;
        private const int ParkingSpacesPerFloor = 50;

        private static readonly Faker Faker = new Faker();

        public static IApplicationBuilder UseTestData(this IApplicationBuilder app, ParkingDbContext dbContext)
        {
            if (!dbContext.Parkings.Any())
            {
                var parkings = Faker.Make(ParkingsCount, GenerateParking);
                dbContext.Parkings.AddRange(parkings);
                dbContext.SaveChanges();
            }

            return app;
        }

        public static Parking GenerateParking()
        {
            var country = Faker.Address.Country();
            var city = Faker.Address.City();
            var street = Faker.Address.StreetName();

            var address = Address.Create(country, city, street);
            var parking = new Parking(address.Value);
            for (var i = 0; i < FloorsPerParking; i++)
            {
                var floor = new Floor(i + 1);
                parking.AddFloor(floor);
                for (var j = 0; j < ParkingSpacesPerFloor; j++)
                {
                    var parkingSpace = new ParkingSpace(j + 1);
                    floor.AddParkingSpace(parkingSpace);
                }
            }

            return parking;
        }
    }
}