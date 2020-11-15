using Parking_Domain.Common;
using Parking_Domain.FunctionalExtensions;

namespace Parking_Domain
{
    public class LicensePlate : ValueObject<LicensePlate>
    {
        public string Value { get; }

        private LicensePlate(string value)
        {
            Value = value;
        }

        protected LicensePlate()
        {
            
        }

        public static Result<LicensePlate> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result<LicensePlate>.Failure("License plate should not be empty");
            }

            value = value.Trim();

            return Result<LicensePlate>.Success(new LicensePlate(value));
        }

        protected override bool EqualsCore(LicensePlate other)
        {
            return string.CompareOrdinal(Value, other.Value) == 0;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}