using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewModels.ParkingLevel
{
    public class ParkVehicleViewModel
    {
        [Required]
        [HiddenInput]
        public int ParkingLevelId { get; set; }

        [Required]
        [HiddenInput]
        public int ParkingSpaceNumber { get; set; }

        [Required]
        public VehicleViewModel Vehicle { get; set; }
    }
}