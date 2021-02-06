using System.Threading;
using System.Threading.Tasks;
using ParkingService.Application.Interfaces;
using ParkingService.Domain.FunctionalExtensions;

namespace ParkingService.Application.Commands
{
    public class DeleteFloorCommand : ICommand<Result>
    {
        public int ParkingId { get; }
        public int Floor { get; }

        public DeleteFloorCommand(int parkingId, int floor)
        {
            ParkingId = parkingId;
            Floor = floor;
        }
    }

    public class DeleteParkingLevelCommandHandler : ICommandHandler<DeleteFloorCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteParkingLevelCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(DeleteFloorCommand request, CancellationToken cancellationToken)
        {
            var parking = unitOfWork.ParkingRepository.Find(request.ParkingId);
            if (parking == null)
            {
                return Task.FromResult(Result.Failure($"Parking with id: {request.ParkingId} not found"));
            }

            var result = parking.RemoveFloor(request.Floor);
            if (!result.IsSuccess)
            {
                return Task.FromResult(result);
            }

            unitOfWork.Save();

            return Task.FromResult(Result.Success());
        }
    }
}
