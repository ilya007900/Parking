using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Parking.Edit
{
    public class ParkingEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Country { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string City { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Street { get; set; }

        public IEnumerable<ParkingLevelEditViewModel> Levels { get; set; }
    }
}