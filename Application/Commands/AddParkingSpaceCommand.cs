using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ParkingService.Application.Interfaces;
using ParkingService.Domain.Entities;
using ParkingService.Domain.FunctionalExtensions;

namespace ParkingService.Application.Commands
{
    public class AddParkingSpaceCommand : ICommand<Result>
    {
        public int ParkingId { get; }
        public int Floor { get; }
        public int SpaceNumber { get; }

        public AddParkingSpaceCommand(int parkingId, int floor, int spaceNumber)
        {
            ParkingId = parkingId;
            Floor = floor;
            SpaceNumber = spaceNumber;
        }
    }

    public class CreateParkingSpaceCommandHandler : ICommandHandler<AddParkingSpaceCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateParkingSpaceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(AddParkingSpaceCommand request, CancellationToken cancellationToken)
        {
            var parking = unitOfWork.ParkingRepository.Find(request.ParkingId);
            var level = parking.Floors.First(x => x.Number == request.Floor);
            var parkingSpace = ParkingSpace.Create(request.SpaceNumber);
            if (!parkingSpace.IsSuccess)
            {
                return Task.FromResult(Result.Failure(parkingSpace.ErrorMessage));
            }

            var result = level.AddParkingSpace(parkingSpace.Value);
            if (!result.IsSuccess)
            {
                return Task.FromResult(Result.Failure(result.ErrorMessage));
            }

            unitOfWork.Save();

            return Task.FromResult(result);
        }
    }

    public class CreateParkingSpaceRequest
    {
        [Required]
        public int Number { get; set; }
    }
}