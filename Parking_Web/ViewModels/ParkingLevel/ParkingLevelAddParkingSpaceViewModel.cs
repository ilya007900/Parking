using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Parking_Web.ViewModels.ParkingLevel
{
    public class ParkingLevelAddParkingSpaceViewModel
    {
        [Required]
        [HiddenInput]
        public int ParkingLevelId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }
    }
}