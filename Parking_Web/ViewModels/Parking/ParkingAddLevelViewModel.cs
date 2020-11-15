using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Parking_Web.ViewModels.Parking
{
    public class ParkingAddLevelViewModel
    {
        [HiddenInput]
        public int ParkingId { get; set; }

        [Required]
        public int Floor { get; set; }
    }
}