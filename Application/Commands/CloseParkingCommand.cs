using ParkingService.Application.Interfaces;
using ParkingService.Domain.FunctionalExtensions;
using ParkingService.Domain.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ParkingService.Application.Commands
{
    public class CloseParkingCommand : ICommand<Result>
    {
        public int ParkingId { get; }
        public string Reason { get; }

        public CloseParkingCommand(int parkingId, string reason = "")
        {
            ParkingId = parkingId;
            Reason = reason;
        }
    }

    public class CloseParkingCommandHandler : ICommandHandler<CloseParkingCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ParkingManager manager;

        public CloseParkingCommandHandler(IUnitOfWork unitOfWork, ParkingManager manager)
        {
            this.unitOfWork = unitOfWork;
            this.manager = manager;
        }

        public Task<Result> Handle(CloseParkingCommand request, CancellationToken cancellationToken)
        {
            var parking = unitOfWork.ParkingRepository.Find(request.ParkingId);
            if (parking == null)
            {
                return Task.FromResult(Result.Failure($"Parking with id: {request.ParkingId} not found"));
            }

            var result = manager.CloseParking(parking, "testing");
            if (!result.IsSuccess)
            {
                return Task.FromResult(Result.Failure(result.ErrorMessage));
            }

            unitOfWork.Save();

            return Task.FromResult(Result.Success());
        }
    }
}