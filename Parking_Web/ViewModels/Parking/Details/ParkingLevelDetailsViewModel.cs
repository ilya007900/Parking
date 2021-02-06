using System.Collections.Generic;

namespace Web.ViewModels.Parking.Details
{
    public class ParkingLevelDetailsViewModel
    {
        public int Id { get; set; }

        public int Floor { get; set; }
        
        public IReadOnlyList<ParkingSpaceDetailsViewModel> ParkingSpaces { get; set; }
    }
}