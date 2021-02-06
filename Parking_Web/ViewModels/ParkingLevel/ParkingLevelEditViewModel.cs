using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ParkingService.Domain.Entities;

namespace Web.ViewModels.ParkingLevel
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