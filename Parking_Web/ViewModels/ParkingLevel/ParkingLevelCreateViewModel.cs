using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.ParkingLevel
{
    public class ParkingLevelCreateViewModel
    {
        [Required]
        public int Floor { get; set; }
    }
}