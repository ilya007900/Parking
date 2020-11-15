using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Parking_Domain;
using Parking_Domain.ParkingLevels;
using Parking_Domain.ParkingSpaces;
using Parking_Web.ViewModels.ParkingLevel;

namespace Parking_Web.Controllers
{
    public class ParkingLevelController : Controller
    {
        private readonly ParkingContext _context;
        private readonly ParkingLevelRepository _levelRepository;

        public ParkingLevelController(ParkingContext context, ParkingLevelRepository levelRepository)
        {
            _context = context;
            _levelRepository = levelRepository;
        }

        [HttpGet]
        public IActionResult Edit(int parkingLevelId)
        {
            var level = _levelRepository.GetById(parkingLevelId);
            if (level == null)
            {
                return NotFound();
            }

            ViewBag.ParkingId = level.Parking.Id;

            var viewModel = new ParkingLevelEditViewModel
            {
                Id = level.Id,
                Floor = level.Floor,
                ParkingSpaces = level.ParkingSpaces.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ParkingLevelEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var level = _context.ParkingLevels.Find(viewModel.Id);
            var result = level.UpdateFloor(viewModel.Floor);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            _context.ParkingLevels.Update(level);
            _context.SaveChanges();

            return RedirectToAction(nameof(ParkingController.Edit), "Parking", new { id = level.Parking.Id });
        }

        [HttpGet]
        public IActionResult AddParkingSpace(int parkingLevelId)
        {
            var viewModel = new ParkingLevelAddParkingSpaceViewModel
            {
                ParkingLevelId = parkingLevelId
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddParkingSpace(ParkingLevelAddParkingSpaceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var level = _levelRepository.GetById(viewModel.ParkingLevelId);
            if (level == null)
            {
                return NotFound();
            }

            var parkingSpace = new ParkingSpace(viewModel.Number);
            var result = level.AddParkingSpace(parkingSpace);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(ParkingController.Edit), "Parking", new {id = level.Parking.Id});
        }

        [HttpGet]
        public IActionResult DeleteParkingSpace(int parkingLevelId, int parkingSpaceId)
        {
            var level = _context.ParkingLevels.Find(parkingLevelId);
            if (level == null)
            {
                return NotFound();
            }

            var parkingSpace = _context.ParkingSpaces.Find(parkingSpaceId);
            if (parkingSpace == null)
            {
                return NotFound();
            }

            level.RemoveParkingSpace(parkingSpace);
            _context.SaveChanges();

            return RedirectToAction(nameof(ParkingController.Edit), "Parking", new { id = level.Parking.Id });
        }

        [HttpGet]
        public IActionResult ParkVehicle(int parkingLevelId, int parkingSpaceNumber)
        {
            var viewModel = new ParkVehicleViewModel
            {
                ParkingLevelId = parkingLevelId,
                ParkingSpaceNumber = parkingSpaceNumber
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ParkVehicle(ParkVehicleViewModel viewModel)
        {
            var level = _levelRepository.GetById(viewModel.ParkingLevelId);
            if (level == null)
            {
                return NotFound();
            }

            var licensePlate = LicensePlate.Create(viewModel.Vehicle.LicensePlate);
            if (!licensePlate.IsSuccess)
            {
                return BadRequest(licensePlate.ErrorMessage);
            }

            var vehicle = Vehicle.Create(licensePlate.Value, viewModel.Vehicle.Weight);
            if (!vehicle.IsSuccess)
            {
                return BadRequest(vehicle.ErrorMessage);
            }

            var result = level.ParkVehicle(viewModel.ParkingSpaceNumber, vehicle.Value);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(ParkingController.Edit), "Parking", new {id = level.Parking.Id});
        }

        [HttpGet]
        public IActionResult FreeParkingSpace(int parkingLevelId, int parkingSpaceNumber)
        {
            var level = _levelRepository.GetById(parkingLevelId);
            if (level == null)
            {
                return NotFound();
            }

            var result = level.FreeParkingSpace(parkingSpaceNumber);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(ParkingController.Edit), "Parking", new {id = level.Parking.Id});
        }
    }
}
