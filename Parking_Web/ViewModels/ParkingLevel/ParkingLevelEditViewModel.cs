using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Parking_Domain.ParkingSpaces;

namespace Parking_Web.ViewModels.ParkingLevel
{
    public class ParkingLevelEditViewModel
    {
        [Required]
        [HiddenInput]
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Floor { get; set; }

        public IReadOnlyList<ParkingSpace> ParkingSpaces { get; set; }
    }
}