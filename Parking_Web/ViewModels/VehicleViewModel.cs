using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class VehicleViewModel
    {
        [Required]
        [StringLength(6, MinimumLength = 4)]
        public string LicensePlate { get; set; }

        [Required]
        [Range(1, 10000)]
        public int Weight { get; set; }
    }
}