using System.ComponentModel.DataAnnotations;

namespace Parking_Web.ViewModels.ParkingLevel
{
    public class ParkingLevelCreateViewModel
    {
        [Required]
        public int Floor { get; set; }
    }
}