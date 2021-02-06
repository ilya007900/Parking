using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewModels.Parking
{
    public class ParkingAddLevelViewModel
    {
        [HiddenInput]
        public int ParkingId { get; set; }

        [Required]
        public int Floor { get; set; }
    }
}