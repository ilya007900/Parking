using Parking_Domain;

namespace Parking_Web.ViewModels.Parking.Details
{
    public class ParkingSpaceDetailsViewModel
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}