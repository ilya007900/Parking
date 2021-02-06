using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Parking
{
    public class ParkingCreateViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Country { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string City { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Street { get; set; }
    }
}