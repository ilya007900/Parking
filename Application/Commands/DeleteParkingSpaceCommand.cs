using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ParkingService.Application.Interfaces;
using ParkingService.Domain.FunctionalExtensions;

namespace ParkingService.Application.Commands
{
    public class DeleteParkingSpaceCommand : ICommand<Result>
    {
        public int ParkingId { get; }
        public int Floor { get; }
        public int SpaceNumber { get; }

        public DeleteParkingSpaceCommand(int parkingId, int floor, int spaceNumber)
        {
            ParkingId = parkingId;
            Floor = floor;
            SpaceNumber = spaceNumber;
        }
    }

    public class DeleteParkingSpaceCommandHandler : ICommandHandler<DeleteParkingSpaceCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteParkingSpaceCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(DeleteParkingSpaceCommand request, CancellationToken cancellationToken)
        {
            var parking = unitOfWork.ParkingRepository.Find(request.ParkingId);
            var level = parking.Floors.First(x => x.Number == request.Floor);
            var result = level.RemoveParkingSpace(request.SpaceNumber);
            if (!result.IsSuccess)
            {
                return Task.FromResult(Result.Failure(result.ErrorMessage));
            }

            unitOfWork.Save();

            return Task.FromResult(Result.Success());
        }
    }
}