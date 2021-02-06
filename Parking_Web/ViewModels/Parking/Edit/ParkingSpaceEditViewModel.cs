using ParkingService.Domain;

namespace Web.ViewModels.Parking.Edit
{
    public class ParkingSpaceEditViewModel
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}