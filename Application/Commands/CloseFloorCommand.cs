using System.Threading;
using System.Threading.Tasks;
using ParkingService.Application.Interfaces;
using ParkingService.Domain.FunctionalExtensions;
using ParkingService.Domain.Services;

namespace ParkingService.Application.Commands
{
    public class CloseFloorCommand : ICommand<Result>
    {
        public int ParkingId { get; }
        public int Floor { get; }

        public CloseFloorCommand(int parkingId, int floor)
        {
            ParkingId = parkingId;
            Floor = floor;
        }
    }

    public class CloseFloorCommandHandler : ICommandHandler<CloseFloorCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ParkingManager manager;

        public CloseFloorCommandHandler(IUnitOfWork unitOfWork, ParkingManager manager)
        {
            this.unitOfWork = unitOfWork;
            this.manager = manager;
        }

        public Task<Result> Handle(CloseFloorCommand request, CancellationToken cancellationToken)
        {
            var parking = unitOfWork.ParkingRepository.Find(request.ParkingId);
            if (parking == null)
            {
                return Task.FromResult(Result.Failure($"Parking with id: {request.ParkingId} not found"));
            }

            var result = manager.CloseFloor(parking, request.Floor);
            if (!result.IsSuccess)
            {
                return Task.FromResult(Result.Failure(result.ErrorMessage));
            }

            unitOfWork.Save();

            return Task.FromResult(Result.Success());
        }
    }
}