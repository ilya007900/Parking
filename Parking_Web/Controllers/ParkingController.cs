using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parking_Web.ViewModels.Parking;
using Parking_Web.ViewModels.Parking.Edit;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using Parking_Web.ViewModels.Parking.Details;

namespace Parking_Web.Controllers
{
    public class ParkingController : Controller
    {
        private const string ApiUrl = "https://localhost:44375/api/parkings";

        private readonly IHttpClientFactory clientFactory;

        public ParkingController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = clientFactory.CreateClient();
            var response = await client.GetStringAsync(ApiUrl);
            var parkings = JsonConvert.DeserializeObject<IReadOnlyList<ParkingDto>>(response);
            return View(parkings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParkingCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(viewModel);
            }

            var dto = new CreateParkingDto
            {
                Country = viewModel.Country,
                City = viewModel.City,
                Street = viewModel.Street
            };
            var json = JsonConvert.SerializeObject(dto);

            var client = clientFactory.CreateClient();
            var response = await client.PostAsync(ApiUrl, new StringContent(json));
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = clientFactory.CreateClient();
            var response = await client.GetStringAsync($"{ApiUrl}/{id}");
            var parking = JsonConvert.DeserializeObject<ParkingInfoDto>(response);
            return View(parking);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ParkingEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(viewModel);
            }

            var dto = new UpdateParkingAddressDto
            {
                Country = viewModel.Country,
                City = viewModel.City,
                Street = viewModel.Street
            };
            var json = JsonConvert.SerializeObject(dto);

            var client = clientFactory.CreateClient();
            var response = await client.PutAsync($"{ApiUrl}/{viewModel.Id}", new StringContent(json, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var client = clientFactory.CreateClient();
            var response = await client.GetStringAsync($"{ApiUrl}/{id}");
            var parking = JsonConvert.DeserializeObject<ParkingInfoDto>(response);
            if (parking == null)
            {
                return NotFound();
            }

            var viewModel = new ParkingDetailsViewModel
            {
                Id = parking.Id,
                City = parking.City,
                Street = parking.Street,
                Country = parking.Country,
                ParkingLevels = parking.Levels.Select(x => new ParkingLevelDetailsViewModel
                {
                    Id = x.Id,
                    Floor = x.Floor,
                    ParkingSpaces = x.Spaces.Select(ps => new ParkingSpaceDetailsViewModel
                    {
                        Id = x.Id,
                        Number = ps.Number,
                        IsFree = ps.IsFree
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
        public async Task<IActionResult> AddLevel(ParkingAddLevelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dto = new AddParkingLevelDto
            {
                Floor = viewModel.Floor
            };
            var json = JsonConvert.SerializeObject(dto);

            var client = clientFactory.CreateClient();
            var response = await client.PostAsync($"{ApiUrl}/{viewModel.ParkingId}/levels", new StringContent(json, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Edit), new { id = viewModel.ParkingId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteLevel(int parkingId, int floor)
        {
            var client = clientFactory.CreateClient();
            var response = await client.DeleteAsync($"{ApiUrl}/{parkingId}/levels/{floor}");
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Edit), new { id = parkingId });
        }

        //[HttpGet]
        //public IActionResult FindParkingSpace(string licensePlate)
        //{
        //    var result = LicensePlate.Create(licensePlate);
        //    if (!result.IsSuccess)
        //    {
        //        return BadRequest(result.ErrorMessage);
        //    }

        //    foreach (var parking in _parkingRepository.GetAll())
        //    {
        //        var parkingSpace = parking.FindParkingSpace(result.Value);
        //        if (parkingSpace != null)
        //        {
        //            return View(parkingSpace);
        //        }
        //    }

        //    return NotFound();
        //}
    }
}
