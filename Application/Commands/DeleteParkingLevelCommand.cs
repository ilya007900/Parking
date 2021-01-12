using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Parking_Domain.FunctionalExtensions;

namespace Application.Commands
{
    public class DeleteParkingLevelCommand : ICommand<Result>
    {
        public int ParkingId { get; }
        public int Floor { get; }

        public DeleteParkingLevelCommand(int parkingId, int floor)
        {
            ParkingId = parkingId;
            Floor = floor;
        }
    }

    public class DeleteParkingLevelCommandHandler : ICommandHandler<DeleteParkingLevelCommand, Result>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteParkingLevelCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(DeleteParkingLevelCommand request, CancellationToken cancellationToken)
        {
            var parking = unitOfWork.ParkingRepository.Find(request.ParkingId);
            if (parking == null)
            {
                return Task.FromResult(Result.Failure(""));
            }

            parking.RemoveParkingLevel(request.Floor);

            unitOfWork.Save();

            return Task.FromResult(Result.Success());
        }
    }
}
