using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.FunctionalExtensions;

namespace Application.Commands
{
    public class CreateParkingSpaceCommand : ICommand<Result>
    {
        public int ParkingId { get; }
        public int Floor { get; }
        public int SpaceNumber { get; }

        public CreateParkingSpaceCommand(int parkingId, int floor, int spaceNumber)
        {
            ParkingId = parkingId;
            Floor = floor;
            SpaceNumber = spaceNumber;
        }
    }

    public class CreateParkingSpaceCommandHandler : ICommandHandler<CreateParkingSpaceCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateParkingSpaceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(CreateParkingSpaceCommand request, CancellationToken cancellationToken)
        {
            var parking = unitOfWork.ParkingRepository.Find(request.ParkingId);
            var level = parking.ParkingLevels.First(x => x.Floor == request.Floor);
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