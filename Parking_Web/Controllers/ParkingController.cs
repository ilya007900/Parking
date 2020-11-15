using Microsoft.AspNetCore.Mvc;
using Parking_Domain;
using Parking_Domain.ParkingEntities;
using Parking_Web.ViewModels.Parking;
using System.Linq;
using Parking_Domain.ParkingLevels;
using Parking_Web.ViewModels.Parking.Edit;
using Parking_Web.ViewModels.Parking.Details;

namespace Parking_Web.Controllers
{
    public class ParkingController : Controller
    {
        private readonly ParkingContext _context;
        private readonly ParkingRepository _parkingRepository;

        public ParkingController(ParkingContext context, ParkingRepository parkingRepository)
        {
            _context = context;
            _parkingRepository = parkingRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var parkingEntities = _context.ParkingEntities.ToArray();
            return View(parkingEntities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ParkingCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(viewModel);
            }

            var address = Address.Create(viewModel.Country, viewModel.City, viewModel.Street);
            if (!address.IsSuccess)
            {
                return BadRequest(address.ErrorMessage);
            }

            var parking = new Parking(address.Value);
            _context.ParkingEntities.Attach(parking);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var parking = _context.ParkingEntities.Find(id);
            if (parking == null)
            {
                return NotFound();
            }

            var viewModel = new ParkingEditViewModel
            {
                Id = id,
                Country = parking.Address.Country,
                City = parking.Address.City,
                Street = parking.Address.Street,
                Levels = parking.ParkingLevels.Select(x => new ParkingLevelEditViewModel
                {
                    Id = x.Id,
                    Floor = x.Floor,
                    ParkingSpaces = x.ParkingSpaces.Select(ps => new ParkingSpaceEditViewModel
                    {
                        Id = ps.Id,
                        Number = ps.Number,
                        Vehicle = ps.Vehicle
                    }).OrderBy(ps => ps.Number).ToList()
                }).OrderBy(x => x.Floor).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ParkingEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(viewModel);
            }

            var parking = _context.ParkingEntities.Find(viewModel.Id);
            if (parking == null)
            {
                return NotFound();
            }

            var address = Address.Create(viewModel.Country, viewModel.City, viewModel.Street);
            if (!address.IsSuccess)
            {
                return BadRequest();
            }

            parking.UpdateAddress(address.Value);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var parking = _context.ParkingEntities.Find(id);
            if (parking == null)
            {
                return NotFound();
            }

            var viewModel = new ParkingDetailsViewModel
            {
                Id = parking.Id,
                City = parking.Address.City,
                Street = parking.Address.Street,
                Country = parking.Address.Country,
                ParkingLevels = parking.ParkingLevels.Select(x => new ParkingLevelDetailsViewModel
                {
                    Id = x.Id,
                    Floor = x.Floor,
                    ParkingSpaces = x.ParkingSpaces.Select(ps=>new ParkingSpaceDetailsViewModel
                    {
                        Id = x.Id,
                        Number = ps.Number,
                        Vehicle = ps.Vehicle
                    }).ToList()
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddLevel(int parkingId)
        {
            var viewModel = new ParkingAddLevelViewModel
            {
                ParkingId = parkingId
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddLevel(ParkingAddLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var parking = _parkingRepository.FindById(viewModel.ParkingId);
            if (parking == null)
            {
                return NotFound();
            }

            var level = new ParkingLevel(viewModel.Floor);
            var result = parking.AddParkingLevel(level);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Edit), new {id = viewModel.ParkingId});
        }

        [HttpGet]
        public IActionResult DeleteLevel(int parkingId, int levelId)
        {
            var parking = _parkingRepository.FindById(parkingId);
            if (parking == null)
            {
                return NotFound();
            }

            var level = parking.ParkingLevels.FirstOrDefault(x => x.Id == levelId);
            if (level == null)
            {
                return NotFound();
            }

            parking.RemoveParkingLevel(level);
            _context.SaveChanges();

            return RedirectToAction(nameof(Edit), new {id = parkingId});
        }

        [HttpGet]
        public IActionResult FindParkingSpace(string licensePlate)
        {
            var result = LicensePlate.Create(licensePlate);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            foreach (var parking in _parkingRepository.GetAll())
            {
                var parkingSpace = parking.FindParkingSpace(result.Value);
                if (parkingSpace != null)
                {
                    return View(parkingSpace);
                }
            }

            return NotFound();
        }
    }
}
