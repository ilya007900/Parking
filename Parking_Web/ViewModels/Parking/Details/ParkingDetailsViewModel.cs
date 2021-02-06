using System.Collections.Generic;

namespace Web.ViewModels.Parking.Details
{
    public class ParkingDetailsViewModel
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public IReadOnlyList<ParkingLevelDetailsViewModel> ParkingLevels { get; set; }
    }
}