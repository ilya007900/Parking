using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Parking_Web.ViewModels.Parking.Edit
{
    public class ParkingLevelEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Floor { get; set; }

        public IReadOnlyList<ParkingSpaceEditViewModel> ParkingSpaces { get; set; }
    }
}