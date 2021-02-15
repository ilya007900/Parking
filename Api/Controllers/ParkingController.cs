using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingService.Application.Commands;
using ParkingService.Application.Queries;

namespace ParkingService.Api.Controllers
{
    [Route("api/parkings")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IMediator mediator;

        public ParkingController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var parkings = await mediator.Send(new GetParkingsQuery());
            return new JsonResult(parkings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var parking = await mediator.Send(new GetParkingQuery(id));
            return new JsonResult(parking);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateParkingRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

            var command = new CreateParkingCommand(dto.Country, dto.City, dto.Street);
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteParkingCommand(id);
            await mediator.Send(command);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] UpdateParkingAddressRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }

            var command = new UpdateParkingAddressCommand(id, dto.Country, dto.City, dto.Street);
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpPut("{id}/close")]
        public async Task<IActionResult> CloseParking(int id)
        {
            var command = new CloseParkingCommand(id);
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpPost("{id}/floors")]
        public async Task<IActionResult> AddFloor(int id, [FromBody] AddFloorRequest dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var command = new AddFloorCommand(id, dto.Number);
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(new { id = result.Value });
        }

        [HttpPut("{id}/floors/{floor}/close")]
        public async Task<IActionResult> CloseFloor(int id, int floor)
        {
            var command = new CloseFloorCommand(id, floor);
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpDelete("{id}/floors/{floor}")]
        public async Task<IActionResult> DeleteFloor(int id, int floor)
        {
            var command = new DeleteFloorCommand(id, floor);
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpPost("{id}/floors/{floor}/spaces")]
        public async Task<IActionResult> CreateParkingSpace(int id, int floor, [FromBody] CreateParkingSpaceRequest dto)
        {
            var command = new AddParkingSpaceCommand(id, floor, dto.Number);
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }

        [HttpDelete("{id}/floors/{floor}/spaces/{number}")]
        public async Task<IActionResult> DeleteParkingSpace(int id, int floor, int number)
        {
            var command = new DeleteParkingSpaceCommand(id, floor, number);
            var result = await mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }
    }
}
