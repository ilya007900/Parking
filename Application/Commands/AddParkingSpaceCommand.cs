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
            var parkingSpace = new ParkingSpace(request.SpaceNumber);
            var result = level.AddParkingSpace(parkingSpace);
            unitOfWork.Save();

            return Task.FromResult(result);
        }
    }

    public class CreateParkingSpaceDto
    {
        public int Number { get; set; }
    }
}