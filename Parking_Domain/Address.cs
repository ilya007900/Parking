using ParkingService.Domain.FunctionalExtensions;

namespace ParkingService.Domain
{
    public class Address
    {
        public static readonly Address BelarusMinskNemiga = new Address("Belarus", "Minsk", "Nemiga");
        public static readonly Address BelarusMinskVostok = new Address("Belarus", "Minsk", "Vostok");

        public string Country { get; }

        public string City { get; }

        public string Street { get; }

        private Address(string country, string city, string street)
        {
            Country = country;
            City = city;
            Street = street;
        }

        protected Address()
        {
            
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {Country}";
        }

        public static Result<Address> Create(string country, string city, string street)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                return Result<Address>.Failure("Country should not be empty");
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                return Result<Address>.Failure("City should not be empty");
            }

            if (string.IsNullOrWhiteSpace(street))
            {
                return Result<Address>.Failure("Street should not be empty");
            }

            return Result<Address>.Success(new Address(country, city, street));
        }
    }
}